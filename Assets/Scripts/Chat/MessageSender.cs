using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class for sending new messages to the chat.
/// </summary>
public class MessageSender : MonoBehaviour
{
    /// <summary>
    /// Link to the <see cref="ChatController"/> instance in the scene.
    /// </summary>
    public ChatController chatController;

    /// <summary>
    /// Link to the <see cref="TMP_InputField"/> component in the scene.
    /// </summary>
    [Header("UI")]
    public TMP_InputField input;

    /// <summary>
    /// Method to send the new message to the chat.
    /// </summary>
    public void SendMessage()
    {
        if (!string.IsNullOrEmpty(input.text))
        {
            MessageData data = new MessageData(chatController.GetRandomMember(), input.text, DateTime.Now.ToString("T"), true);

            chatController.AddMessage(data);

            input.text = "";
        }
    }
}
