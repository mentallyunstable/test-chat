using System;

[Serializable]
public struct ChatItemData
{
    public MessageData messageData;
    public MessageRemover remover;
    public AvatarLoader avatarLoader;
    public string playerId;

    public ChatItemData(MessageData messageData, MessageRemover remover, AvatarLoader avatarLoader, string playerId)
    {
        this.messageData = messageData;
        this.remover = remover;
        this.avatarLoader = avatarLoader;
        this.playerId = playerId;
    }
}