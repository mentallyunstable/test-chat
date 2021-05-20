using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageSender : MonoBehaviour
{
    public ChatController chatController;

    [Header("UI")]
    public TMP_InputField input;

    public void SendMessage()
    {
        if (!string.IsNullOrEmpty(input.text))
        {
            MessageData data = new MessageData(chatController.playerId, input.text, DateTime.Now.ToString("T"));

            chatController.AddMessage(data);

            input.text = "";
        }
    }
}
