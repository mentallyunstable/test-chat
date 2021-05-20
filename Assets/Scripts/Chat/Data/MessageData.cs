using System;

[Serializable]
public struct MessageData
{
    public string sender;
    public string message;
    public string date;

    public MessageData(string sender, string message, string date)
    {
        this.sender = sender;
        this.message = message;
        this.date = date;
    }
}
