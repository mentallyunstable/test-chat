using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MessagesHandler : MonoBehaviour
{
    /// <summary>
    /// Instance of the <see cref="AvatarLoader"/> that will be injected to instantiated chat item.
    /// </summary>
    public AvatarLoader avatarLoader;
    /// <summary>
    /// Instance of the <see cref="MessageRemover"/> that will be injected to instantiated chat item.
    /// </summary>
    public MessageRemover remover;

    /// <summary>
    /// Link to the <see cref="Transform"/> component in the scene storing all chat items.
    /// </summary>
    [Header("UI")]
    public Transform messagesParent;
    /// <summary>
    /// Link to the <see cref="VerticalLayoutGroup"/> component in the scene, controlling chat items position.
    /// </summary>
    public VerticalLayoutGroup verticalLayout;

    /// <summary>
    /// Prefab of the main message item.
    /// </summary>
    [Header("Prefabs")]
    public ChatMessage mainMessagePrefab;
    /// <summary>
    /// Prefab of the secondary message item.
    /// </summary>
    public ChatMessage secondaryMessagePrefab;
    /// <summary>
    /// Prefab of the main message item of the player.
    /// </summary>
    public ChatMessage mainMessagePlayerPrefab;
    /// <summary>
    /// Prefab of the secondary message item of the player.
    /// </summary>
    public ChatMessage secondaryMessagePlayerPrefab;

    /// <summary>
    /// List of all chat items.
    /// </summary>
    public List<ChatMessage> messages = new List<ChatMessage>();
    /// <summary>
    /// Last message sender.
    /// </summary>
    private string lastSender;
    /// <summary>
    /// Indicates whether messages parent should be updated on the next frame.
    /// </summary>
    private bool waitForUpdate;

    private void Awake()
    {
        remover.OnMessageRemoveAccepted += Remover_OnMessageDeleteAccepted;
    }

    private void Update()
    {
        /// Updates the chat UI on the next frame
        if (waitForUpdate)
        {
            verticalLayout.enabled = true;
            verticalLayout.CalculateLayoutInputVertical();
            verticalLayout.SetLayoutVertical();
            LayoutRebuilder.ForceRebuildLayoutImmediate(messagesParent as RectTransform);
            Canvas.ForceUpdateCanvases();

            waitForUpdate = false;
        }
    }

    /// <summary>
    /// Spawning all messages by data.
    /// </summary>
    /// <param name="data">All messages data.</param>
    /// <param name="playerId">Current player id</param>
    public void SpawnMessages(List<MessageData> data, string playerId)
    {
        lastSender = "";
        for (int i = 0; i < data.Count; i++)
        {
            GetNewMessage(data[i], playerId);
        }


        UpdateChatUI();
    }

    /// <summary>
    /// Spawn new message.
    /// </summary>
    /// <param name="data">New message data.</param>
    /// <param name="playerId">Current player id.</param>
    public void SpawnNewMessage(MessageData data, string playerId)
    {
        GetNewMessage(data, playerId);

        UpdateChatUI();
    }

    /// <summary>
    /// Instantiating new message.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="playerId"></param>
    /// <returns></returns>
    private ChatMessage GetNewMessage(MessageData data, string playerId)
    {
        ChatMessage message;

        /// If new message is main, instantiate it, else instantiate secondary
        if (IsMainMessage(data.sender))
        {
            message = InstantiateMainMessage(data, playerId);
        }
        else
        {
            message = InstantiateSecondaryMessage(data, playerId);
        }
        message.Setup(new ChatItemData(data, remover, avatarLoader, playerId));
        messages.Add(message);

        lastSender = data.sender;

        return message;
    }

    /// <summary>
    /// Instantiating new main message.
    /// </summary>
    /// <param name="data">Instantiating message data.</param>
    /// <param name="playerId">Current player id.</param>
    /// <returns></returns>
    private ChatMessage InstantiateMainMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? mainMessagePlayerPrefab : mainMessagePrefab, messagesParent);

    private ChatMessage InstantiateSecondaryMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? secondaryMessagePlayerPrefab : secondaryMessagePrefab, messagesParent);

    /// <summary>
    /// Method that updates chat UI.
    /// </summary>
    private void UpdateChatUI()
    {
        verticalLayout.CalculateLayoutInputVertical();
        verticalLayout.SetLayoutVertical();
        LayoutRebuilder.ForceRebuildLayoutImmediate(messagesParent as RectTransform);
        verticalLayout.enabled = false;
        waitForUpdate = true;
    }

    /// <summary>
    /// Is last message sender same as current.
    /// </summary>
    /// <param name="sender">Current message sender.</param>
    /// <returns></returns>
    private bool IsMainMessage(string sender) => lastSender != sender;

    /// <summary>
    /// Method that removes all chat items that have been chosen for remove.
    /// </summary>
    /// <param name="removedMessages"></param>
    private void RemoveMessages(List<ChatMessage> removedMessages)
    {
        /// Is some main message have been chosen for remove
        bool isRemovingMain = false;
        for (int i = 0; i < removedMessages.Count; i++)
        {
            removedMessages[i].DestroyMessage();
            if (removedMessages[i] is MainMessage || isRemovingMain)
            {
                /// Get removed main message index
                int mainIndex = messages.IndexOf(removedMessages[i]);
                isRemovingMain = true;
                /// If main message is last in the chat, we don't want to swap it
                if (mainIndex + 1 < messages.Count)
                {
                    ChatMessage nextMessage = messages[mainIndex + 1];

                    /// If next message have been chosen for remove or next message has another sender, we don't want to swap it
                    if (!removedMessages.Contains(nextMessage) && nextMessage.Data.sender == removedMessages[i].Data.sender)
                    {
                        SwapSecondaryToMain(nextMessage);
                    }
                }
            }
            messages.Remove(removedMessages[i]);
        }
    }

    /// <summary>
    /// Swapes secondary message with the removed main message.
    /// </summary>
    /// <param name="nextMessage">Secondary message that should be swapped.</param>
    private void SwapSecondaryToMain(ChatMessage nextMessage)
    {
        /// Instantiate new main message
        ChatMessage newMainMessage = InstantiateMainMessage(nextMessage.Data, nextMessage.Data.sender);

        /// Change data that new message will not be animated
        MessageData newData = nextMessage.Data;
        newData.animate = false;

        /// Setup new message
        newMainMessage.Setup(new ChatItemData(newData, remover, avatarLoader, newData.sender));
        /// Set sibling of the new message to the removed main message
        newMainMessage.transform.SetSiblingIndex(nextMessage.transform.GetSiblingIndex());

        /// If new message is last in the chat, add to end of the list, else insert by removed main message index
        if (nextMessage.transform.GetSiblingIndex() >= messages.Count)
        {
            messages.Add(newMainMessage);
        }
        else
        {
            messages.Insert(nextMessage.transform.GetSiblingIndex(), newMainMessage);
        }

        nextMessage.DestroyMessage();

        messages.Remove(nextMessage);
    }

    /// <summary>
    /// Subscription to the 
    /// </summary>
    /// <param name="removedMessages"></param>
    private void Remover_OnMessageDeleteAccepted(List<ChatMessage> removedMessages)
    {
        RemoveMessages(removedMessages);

        lastSender = messages[messages.Count - 1].Data.sender;

        UpdateChatUI();
    }
}
