using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class ChatMessage : MonoBehaviour
{
    private const string showTrigger = "show";

    readonly Color deleteButtonActiveColor = new Color(255, 255, 255, 1);
    readonly Color deleteButtonDisabledColor = new Color(255, 255, 255, 0.5f);

    [Header("Base Message")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI dateText;
    public Image deleteButton;

    public MessageData Data { get; private set; }

    protected AvatarLoader avatarLoader;

    private MessageRemover remover;
    private Animator animator;
    private bool selectedForRemove;

    protected abstract void OnSetup();

    public void Setup(ChatItemData data)
    {
        Data = data.messageData;
        avatarLoader = data.avatarLoader;
        remover = data.remover;

        messageText.text = Data.message;
        dateText.text = Data.date;

        animator = GetComponent<Animator>();

        if (Data.animate)
        {
            animator.SetTrigger(showTrigger);
        }

        if (Data.sender == data.playerId)
        {
            Subscribe();
        }

        OnSetup();
    }

    private void Subscribe()
    {
        remover.OnMessageDeleteInitiated += Remover_OnMessageDeleteInitiated;
        remover.OnMessageDeleteAccepted += Remover_OnMessageDeleteAccepted;
    }

    public void DestroyMessage()
    {
        remover.OnMessageDeleteInitiated -= Remover_OnMessageDeleteInitiated;
        remover.OnMessageDeleteAccepted -= Remover_OnMessageDeleteAccepted;

        Destroy(gameObject);
    }

    private void Remover_OnMessageDeleteInitiated()
    {
        EnableDeleteButton();
    }

    private void EnableDeleteButton()
    {
        deleteButton.gameObject.SetActive(true);
    }

    private void DisableDeleteButton()
    {
        deleteButton.gameObject.SetActive(false);
    }

    public void ChangeRemoveState()
    {
        selectedForRemove = !selectedForRemove;

        if (selectedForRemove)
        {
            remover.AddToList(this);
        }
        else
        {
            remover.RemoveFromList(this);
        }

        ChangeDeleteButtonState();
    }

    private void Remover_OnMessageDeleteAccepted(List<ChatMessage> messages)
    {
        if (!messages.Contains(this))
        {
            selectedForRemove = false;
            ChangeDeleteButtonState();
            DisableDeleteButton();
        }
    }

    private void ChangeDeleteButtonState()
    {
        deleteButton.color = selectedForRemove ? deleteButtonDisabledColor : deleteButtonActiveColor;
    }
}
