using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class for storing chat configuration.
/// </summary>
public class ChatController : MonoBehaviour
{
    /// <summary>
    /// Link to the <see cref="MessagesHandler"/> instance in the scene.
    /// </summary>
    public MessagesHandler messagesSpawner;

    /// <summary>
    /// Indicates current player id.
    /// </summary>
    [Header("Data")]
    public string playerId;
    /// <summary>
    /// Storing all chat members.
    /// </summary>
    public List<string> senders = new List<string>();
    /// <summary>
    /// Stroing all chat messages.
    /// </summary>
    public List<MessageData> messageData = new List<MessageData>();

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Method, that will be called on the app start.
    /// </summary>
    public void Initialize()
    {
        messagesSpawner.SpawnMessages(messageData, playerId);
    }

    /// <summary>
    /// Adds new message to the chat.
    /// </summary>
    /// <param name="data">Data of the new message.</param>
    public void AddMessage(MessageData data)
    {
        messageData.Add(data);

        messagesSpawner.SpawnNewMessage(data, playerId);
    }

    /// <summary>
    /// Returning random chat member.
    /// </summary>
    /// <returns></returns>
    public string GetRandomMember() => senders[Random.Range(0, senders.Count - 1)];
}
