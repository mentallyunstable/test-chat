using System;

[Serializable]
public struct ChatItemData
{
    /// <summary>
    /// Instance of <see cref="MessageData"/> of the message.
    /// </summary>
    public MessageData messageData;
    /// <summary>
    /// Instance of <see cref="MessageRemover"/> for the <see cref="ChatMessage"/> initialization.
    /// </summary>
    public MessageRemover remover;
    /// <summary>
    /// Instance of <see cref="AvatarLoader"/> for the <see cref="ChatMessage"/> initialization.
    /// </summary>
    public AvatarLoader avatarLoader;
    /// <summary>
    /// Indicates the current player id.
    /// </summary>
    public string playerId;

    /// <summary>
    /// Constructor for <see cref="ChatItemData"/> instance.
    /// </summary>
    /// <param name="messageData">Instance of <see cref="MessageData"/> of the message.</param>
    /// <param name="remover">Instance of <see cref="MessageRemover"/> for the <see cref="ChatMessage"/> initialization.</param>
    /// <param name="avatarLoader">Instance of <see cref="AvatarLoader"/> for the <see cref="ChatMessage"/> initialization.</param>
    /// <param name="playerId">Indicates the current player id.</param>
    public ChatItemData(MessageData messageData, MessageRemover remover, AvatarLoader avatarLoader, string playerId)
    {
        this.messageData = messageData;
        this.remover = remover;
        this.avatarLoader = avatarLoader;
        this.playerId = playerId;
    }
}