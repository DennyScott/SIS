using UnityEngine;
using System.Collections;
using SISConnect.Resources;
using Zephyr.EventSystem.Core;

public class IncomingSISEvent : GameEvent
{
    public SocketMessage Message;

    public IncomingSISEvent(SocketMessage message)
    {
        Message = message;
    }
}
