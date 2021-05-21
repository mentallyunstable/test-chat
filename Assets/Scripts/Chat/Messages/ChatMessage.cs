using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Abstraction for all chat messages.
/// </summary>
public abstract class ChatMessage : MonoBehaviour
{
    /// <summary>
    /// Animation trigger for message item show animation.
    /// </summary>
    private const string showTrigger = "show";

    /// <summary>
    /// Color for button, when message is not have been chosen for remove.
    /// </summary>
    readonly Color deleteButtonActiveColor = new Color(255, 255, 255, 1);
    /// <summary>
    /// Color for button, when message have been chosen for remove.
    /// </summary>
    readonly Color deleteButtonDisabledColor = new Color(255, 255, 255, 0.5f);

    /// <summary>
    /// Link to the <see cref="TextMeshProUGUI"/> component in the scene, that will have text of the message.
    /// </summary>
    [Header("Base Message")]
    public TextMeshProUGUI messageText;
    /// <summary>
    /// Link to the <see cref="TextMeshProUGUI"/> component in the scene, that will have date of the message.
    /// </summary>
    public TextMeshProUGUI dateText;
    /// <summary>
    /// Link to the <see cref="Image"/> component in the scene of the remove button.
    /// </summary>
    public Image removeButton;

    /// <summary>
    /// Instance of the <see cref="MessageData"/> of the message, that will be injected on the <see cref="Setup"/> method.
    /// </summary>
    public MessageData Data { get; private set; }

    /// <summary>
    /// Instance of the <see cref="AvatarLoader"/> that will be injected on the <see cref="Setup"/> method.
    /// </summary>
    protected AvatarLoader avatarLoader;

    /// <summary>
    /// Instance of the <see cref="MessageRemover"/> that will be injected on the <see cref="Setup"/> method.
    /// </summary>
    private MessageRemover remover;
    /// <summary>
    /// Link to the <see cref="Animator"/> component of the item.
    /// </summary>
    private Animator animator;
    /// <summary>
    /// Indicates whether item selected for remove.
    /// </summary>
    private bool isSelectedForRemove;

    /// <summary>
    /// Abstract method, that is called in the <see cref="Setup"/> method.
    /// </summary>
    protected abstract void OnSetup();

    /// <summary>
    /// Called on instantiation of the message item.
    /// </summary>
    /// <param name="data">Data for the message initialization.</param>
    public void Setup(ChatItemData data)
    {
        Data = data.messageData;
        avatarLoader = data.avatarLoader;
        remover = data.remover;

        messageText.text = Data.text;
        dateText.text = Data.date;

        /// Subscribe only if this is a player message.
        if (Data.sender == data.playerId)
        {
            Subscribe();
        }

        ShowAnimation();

        OnSetup();
    }

    /// <summary>
    /// Animating chat item if it's player message.
    /// </summary>
    private void ShowAnimation()
    {
        animator = GetComponent<Animator>();

        if (Data.animate)
        {
            animator.SetTrigger(showTrigger);
        }
    }

    /// <summary>
    /// Subscribes the message item on events.
    /// </summary>
    private void Subscribe()
    {
        remover.OnMessageRemoveInitiated += Remover_OnMessageDeleteInitiated;
        remover.OnMessageRemoveAccepted += Remover_OnMessageDeleteAccepted;
    }

    /// <summary>
    /// Unsubscribes the message item from events.
    /// </summary>
    private void Unsubscribe()
    {
        remover.OnMessageRemoveInitiated -= Remover_OnMessageDeleteInitiated;
        remover.OnMessageRemoveAccepted -= Remover_OnMessageDeleteAccepted;
    }

    /// <summary>
    /// Destroying the item.
    /// </summary>
    public void DestroyMessage()
    {
        Unsubscribe();

        Destroy(gameObject);
    }

    /// <summary>
    /// Enabling remove button.
    /// </summary>
    private void EnableRemoveButton()
    {
        removeButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Disabling remove button.
    /// </summary>
    private void DisableRemoveButton()
    {
        removeButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Changing item remove state.
    /// </summary>
    public void ChangeRemoveState()
    {
        isSelectedForRemove = !isSelectedForRemove;

        /// If selected for remove, add to the remove list, if not, remove from list.
        if (isSelectedForRemove)
        {
            remover.AddToList(this);
        }
        else
        {
            remover.RemoveFromList(this);
        }

        ChangeDeleteButtonState();
    }

    /// <summary>
    /// Subscription method to the <see cref="MessageRemover.OnMessageRemoveInitiatedEvent"/> event.
    /// </summary>
    private void Remover_OnMessageDeleteInitiated()
    {
        EnableRemoveButton();
    }

    /// <summary>
    /// Subscription method to the <see cref="MessageRemover.OnMessageRemoveAcceptEvent"/> event.
    /// </summary>
    /// <param name="messages">Messages that have been chosen for remove.</param>
    private void Remover_OnMessageDeleteAccepted(List<ChatMessage> messages)
    {
        if (!messages.Contains(this))
        {
            isSelectedForRemove = false;
            ChangeDeleteButtonState();
            DisableRemoveButton();
        }
    }

    /// <summary>
    /// Method for changing the <see cref="removeButton"/>
    /// </summary>
    private void ChangeDeleteButtonState()
    {
        removeButton.color = isSelectedForRemove ? deleteButtonDisabledColor : deleteButtonActiveColor;
    }
}
