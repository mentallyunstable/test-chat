using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageRemover : MonoBehaviour
{
    /// <summary>
    /// Delegate for all <see cref="OnMessageRemoveInitiatedEvent"/> events.
    /// </summary>
    public delegate void OnMessageRemoveInitiatedEvent();
    /// <summary>
    /// This event will be fired on <see cref="InitiateRemove"/> method.
    /// </summary>
    public event OnMessageRemoveInitiatedEvent OnMessageRemoveInitiated;

    /// <summary>
    /// Delegate for all <see cref="OnMessageRemoveAccepted"/> events.
    /// </summary>
    /// <param name="messages">Messages, that have been chosen for remove.</param>
    public delegate void OnMessageRemoveAcceptEvent(List<ChatMessage> messages);
    /// <summary>
    /// This event will be fired on <see cref="AcceptRemove"/> method.
    /// </summary>
    public event OnMessageRemoveAcceptEvent OnMessageRemoveAccepted;

    /// <summary>
    /// Messages, that will be send on <see cref="OnMessageRemoveAccepted"/> event.
    /// </summary>
    private readonly List<ChatMessage> messages = new List<ChatMessage>();

    /// <summary>
    /// Calling all the <see cref="OnMessageRemoveInitiatedEvent"/> events.
    /// </summary>
    public void InitiateRemove()
    {
        OnMessageRemoveInitiated?.Invoke();
    }

    /// <summary>
    /// Calling all the <see cref="OnMessageRemoveAcceptEvent"/> events.
    /// </summary>
    public void AcceptRemove()
    {
        OnMessageRemoveAccepted?.Invoke(messages);
        messages.Clear();
    }

    /// <summary>
    /// Adding <see cref="ChatMessage"/> to the list of removing messages.
    /// </summary>
    /// <param name="message">Message, that will be added to the remove list.</param>
    public void AddToList(ChatMessage message)
    {
        messages.Add(message);
    }

    /// <summary>
    /// Removing <see cref="ChatMessage"/> from the list of removing messages.
    /// </summary>
    /// <param name="message">Message, that will be removed from the remove list.</param>
    public void RemoveFromList(ChatMessage message)
    {
        messages.Remove(message);
    }
}
