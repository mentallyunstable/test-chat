using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSender : MonoBehaviour
{
    public ChatController chatController;

    [Header("UI")]
    public InputField input;

    public void SendMessage()
    {
        MessageData data = new MessageData(chatController.playerId, input.text, DateTime.Now.ToString("T"));

        chatController.AddMessage(data);
    }
}
