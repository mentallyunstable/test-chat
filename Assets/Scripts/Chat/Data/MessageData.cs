using System;

/// <summary>
/// Stores the data of message.
/// </summary>
[Serializable]
public struct MessageData
{
    /// <summary>
    /// Indicates the sender of the message.
    /// </summary>
    public string sender;
    /// <summary>
    /// Indicates the text of the message.
    /// </summary>
    public string text;
    /// <summary>
    /// Indicates the date of the message.
    /// </summary>
    public string date;
    /// <summary>
    /// Indicates, whether the message should be animated on instantiate.
    /// </summary>
    public bool animate;

    /// <summary>
    /// Constructor for <see cref="MessageData"/> instance.
    /// </summary>
    /// <param name="sender">Sender of the message.</param>
    /// <param name="text">Text of the message.</param>
    /// <param name="date">Date of the message.</param>
    /// <param name="animate">Indicates, whether the message should be animated on instantiate.</param>
    public MessageData(string sender, string text, string date, bool animate)
    {
        this.sender = sender;
        this.text = text;
        this.date = date;
        this.animate = animate;
    }
}
