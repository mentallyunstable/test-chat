using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Subclass of the abstract <see cref="ChatMessage"/> class that will have avatar and chat member name.
/// </summary>
public class MainMessage : ChatMessage
{
    /// <summary>
    /// Link to the <see cref="Image"/> component in the scene with sender avatar.
    /// </summary>
    [Header("Main Message")]
    public Image avatarImage;
    /// <summary>
    /// Link to the <see cref="TextMeshProUGUI"/> component in the scene showing the sender of the message.
    /// </summary>
    public TextMeshProUGUI nameText;

    /// <summary>
    /// Overrided method of <see cref="ChatMessage.OnSetup"/> abstraction.
    /// </summary>
    protected override void OnSetup()
    {
        avatarImage.sprite = avatarLoader.GetAvatar(Data.sender);
        nameText.text = Data.sender;
    }
}
