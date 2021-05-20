using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRemover : MonoBehaviour
{
    public delegate void OnMessageDeleteInitiatedEvent();
    public event OnMessageDeleteInitiatedEvent OnMessageDeleteInitiated;

    public delegate void OnMessageDeleteAcceptEvent(List<ChatMessage> messages);
    public event OnMessageDeleteAcceptEvent OnMessageDeleteAccepted;

    private readonly List<ChatMessage> messages = new List<ChatMessage>();

    public void InitiateRemove()
    {
        OnMessageDeleteInitiated?.Invoke();
    }

    public void AcceptRemove()
    {
        OnMessageDeleteAccepted?.Invoke(messages);
        messages.Clear();
    }

    public void AddToList(ChatMessage message)
    {
        messages.Add(message);
    }

    public void RemoveFromList(ChatMessage message)
    {
        messages.Remove(message);
    }
}
