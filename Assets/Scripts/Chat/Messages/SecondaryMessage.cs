/// <summary>
/// Subclass of the abstract <see cref="ChatMessage"/> class that will not have avatar and chat member name.
/// </summary>
public class SecondaryMessage : ChatMessage
{
    protected override void OnSetup()
    {
        /// Некий задел на будущую расширяемость функционала чата
    }
}
