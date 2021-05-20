using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ChatMessage : MonoBehaviour
{
    [Header("Base Message")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI dateText;

    protected AvatarLoader avatarLoader;

    protected abstract void OnSetup(MessageData data);

    public void Setup(MessageData data, AvatarLoader avatarLoader)
    {
        this.avatarLoader = avatarLoader;

        messageText.text = data.message;
        dateText.text = data.date;

        OnSetup(data);
    }
}
