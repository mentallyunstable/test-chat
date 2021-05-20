using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesHandler : MonoBehaviour
{
    public AvatarLoader avatarLoader;

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
            SpawnNewMessage(data[i], playerId);
        }

        UpdateChatUI();
    }

    private ChatMessage SpawnMainMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? mainMessagePlayerPrefab : mainMessagePrefab, messagesParent);

    private ChatMessage SpawnSecondaryMessage(MessageData data, string playerId) => Instantiate(playerId == data.sender ? secondaryMessagePlayerPrefab : secondaryMessagePrefab, messagesParent);

    public void SpawnNewMessage(MessageData data, string playerId)
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

        message.Setup(data, avatarLoader);
        messages.Add(message);

        lastSender = data.sender;
    }

    private void UpdateChatUI()
    {
        verticalLayout.enabled = false;
        verticalLayout.enabled = true;
        Canvas.ForceUpdateCanvases();
    }
}
