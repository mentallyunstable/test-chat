using System;

[Serializable]
public struct MessageData
{
    public string sender;
    public string message;
    public string date;
    public bool animate;

    public MessageData(string sender, string message, string date, bool animate)
    {
        this.sender = sender;
        this.message = message;
        this.date = date;
        this.animate = animate;
    }
}
