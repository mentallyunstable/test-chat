using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class ChatMessage : MonoBehaviour
{
    readonly Color deleteButtonActiveColor = new Color(255, 255, 255, 1);
    readonly Color deleteButtonDisabledColor = new Color(255, 255, 255, 0.5f);

    [Header("Base Message")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI dateText;
    public Image deleteButton;

    protected AvatarLoader avatarLoader;

    private MessageRemover remover;
    private bool selectedForRemove;

    protected abstract void OnSetup(MessageData data);

    public void Setup(ChatItemData data)
    {
        avatarLoader = data.avatarLoader;
        remover = data.remover;

        messageText.text = data.messageData.message;
        dateText.text = data.messageData.date;

        if (data.messageData.sender == data.playerId)
        {
            data.remover.OnMessageDeleteInitiated += Remover_OnMessageDeleteInitiated;
        }

        OnSetup(data.messageData);
    }

    private void Remover_OnMessageDeleteInitiated()
    {
        EnableDeleteButton();
    }

    private void EnableDeleteButton()
    {
        deleteButton.gameObject.SetActive(true);
    }

    public void ChangeRemoveState()
    {
        selectedForRemove = !selectedForRemove;

        if (selectedForRemove)
        {
            remover.AddToRemoveList(this);
        }
        else
        {
            remover.RemoveFromRemoveList(this);
        }

        ChangeDeleteButtonState();
    }

    private void ChangeDeleteButtonState()
    {
        deleteButton.color = selectedForRemove ? deleteButtonDisabledColor : deleteButtonActiveColor;
    }
}
