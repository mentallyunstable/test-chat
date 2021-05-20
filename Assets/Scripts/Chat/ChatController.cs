using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    public MessagesHandler messagesSpawner;

    [Header("Data")]
    public string playerId;
    public List<string> senderes = new List<string>();
    public List<MessageData> messageData = new List<MessageData>();

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        messagesSpawner.SpawnMessages(messageData, playerId);
    }

    public void AddMessage(MessageData data)
    {
        messageData.Add(data);

        messagesSpawner.SpawnNewMessage(data, playerId);
    }
}
