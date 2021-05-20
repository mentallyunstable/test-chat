using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MessagesHandler : MonoBehaviour
{
    public AvatarLoader avatarLoader;
    public MessageRemover remover;

    [Header("UI")]
    public Transform messagesParent;
    public VerticalLayoutGroup verticalLayout;

    [Header("Prefabs")]
    public ChatMessage mainMessagePrefab;
    public ChatMessage secondaryMessagePrefab;
    public ChatMessage mainMessagePlayerPrefab;
    public ChatMessage secondaryMessagePlayerPrefab;

    public List<ChatMessage> messages = new List<ChatMessage>();
    private int lastMainMessageIndex;
    private string lastSender;
    private bool waitForUpdate;

    private void Awake()
    {
        remover.OnMessageDeleteAccepted += Remover_OnMessageDeleteAccepted;
    }

    private void Update()
    {
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

    private void Remover_OnMessageDeleteAccepted(List<ChatMessage> removedMessages)
    {
        for (int i = 0; i < removedMessages.Count; i++)
        {
            removedMessages[i].DestroyMessage();
            if (removedMessages[i] is MainMessage)
            {
                int mainIndex = messages.IndexOf(removedMessages[i]);
                if (mainIndex + 1 < messages.Count)
                {
                    ChatMessage nextMessage = messages[mainIndex + 1];

                    if (nextMessage.Data.sender == removedMessages[i].Data.sender)
                    {
                        ChatMessage newMainMessage = InstantiateMainMessage(nextMessage.Data, nextMessage.Data.sender);
                        MessageData newData = nextMessage.Data;
                        newData.animate = false;
                        newMainMessage.Setup(new ChatItemData(newData, remover, avatarLoader, newData.sender));
                        newMainMessage.transform.SetSiblingIndex(nextMessage.transform.GetSiblingIndex());
                        messages.Insert(nextMessage.transform.GetSiblingIndex(), newMainMessage);

                        nextMessage.DestroyMessage();

                        messages.Remove(nextMessage);
                    }
                }
            }
            messages.Remove(removedMessages[i]);
        }

        lastSender = messages[messages.Count - 1].Data.sender;

        UpdateChatUI();
    }

    public void SpawnMessages(List<MessageData> data, string playerId)
    {
        lastSender = "";
        for (int i = 0; i < data.Count; i++)
        {
            SpawnMessage(data[i], playerId);
        }


        UpdateChatUI();
    }
    private ChatMessage InstantiateMainMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? mainMessagePlayerPrefab : mainMessagePrefab, messagesParent);

    private ChatMessage InstantiateSecondaryMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? secondaryMessagePlayerPrefab : secondaryMessagePrefab, messagesParent);

    public ChatMessage SpawnMessage(MessageData data, string playerId)
    {
        ChatMessage message;

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

    public void SpawnNewMessage(MessageData data, string playerId)
    {
        SpawnMessage(data, playerId);

        UpdateChatUI();
    }

    private void UpdateChatUI()
    {
        verticalLayout.CalculateLayoutInputVertical();
        verticalLayout.SetLayoutVertical();
        LayoutRebuilder.ForceRebuildLayoutImmediate(messagesParent as RectTransform);
        verticalLayout.enabled = false;
        waitForUpdate = true;
        //verticalLayout.enabled = true;
        //Canvas.ForceUpdateCanvases();
    }

    private bool IsMainMessage(string sender) => lastSender != sender;
}
