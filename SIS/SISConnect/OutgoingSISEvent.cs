using Zephyr.EventSystem.Core;

public class OutgoingSISEvent : GameEvent
{
    public OutgoingSISEvent(string command, string message)
    {
        Command = command;
        Message = message;
    }

    public string Command;
    public string Message;

}
