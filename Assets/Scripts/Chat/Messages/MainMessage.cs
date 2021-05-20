﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMessage : ChatMessage
{
    [Header("Main Message")]
    public Image avatarImage;
    public TextMeshProUGUI nameText;

    protected override void OnSetup(MessageData data)
    {
        avatarImage.sprite = avatarLoader.GetAvatar(data.sender);
        nameText.text = data.sender;
    }
}