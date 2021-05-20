using System.Collections;
using System.Collections.Generic;
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

    private readonly List<ChatMessage> messages = new List<ChatMessage>();
    private string lastSender;

    public void SpawnMessages(List<MessageData> data, string playerId)
    {
        lastSender = "";
        for (int i = 0; i < data.Count; i++)
        {
            SpawnMessage(data[i], playerId);
        }

        UpdateChatUI();
    }

    private ChatMessage SpawnMainMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? mainMessagePlayerPrefab : mainMessagePrefab, messagesParent);

    private ChatMessage SpawnSecondaryMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? secondaryMessagePlayerPrefab : secondaryMessagePrefab, messagesParent);

    public void SpawnMessage(MessageData data, string playerId)
    {
        ChatMessage message;
        if (lastSender != data.sender)
        {
            message = SpawnMainMessage(data, playerId);
        }
        else
        {
            message = SpawnSecondaryMessage(data, playerId);
        }

        message.Setup(new ChatItemData(data, remover, avatarLoader, playerId));
        messages.Add(message);

        lastSender = data.sender;
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
        //LayoutRebuilder.ForceRebuildLayoutImmediate(messagesParent as RectTransform);
        //verticalLayout.enabled = false;
        //verticalLayout.enabled = true;
        //Canvas.ForceUpdateCanvases();
    }
}
