using SISConnect.Resources;
using SISConnect.SharpConnect;
using UnityEngine;
using Zephyr.EventSystem.Core;

public class SISClient : MonoBehaviour
{
    public Connector SisConnection = new Connector();
    string lastMessage;
    public Transform PlayerCoord;
    public string PlayerName = "Annonymous";
    public string Address = "127.0.0.1";

    void Start()
    {
        PlayerName = PlayerPrefs.GetString("PlayerName", "Annonymous");
        Address = PlayerPrefs.GetString("Address", "127.0.0.1");
        Debug.Log(SisConnection.Connect(Address, 10000, PlayerName, OnCommand));
        if (SisConnection.res != "")
        {
            Debug.Log(SisConnection.res);
        }

        EventManager.Instance.AddListener<OutgoingSISEvent>(OutgoingEvent);

    }

    void OnCommand(SocketMessage message)
    {
        EventManager.Instance.QueueEvent(new IncomingSISEvent(message));
        Debug.Log("Zephyr Event Manager here: " + message.Command + "-" + message.Message);
    }

    void OutgoingEvent(OutgoingSISEvent evt)
    {
       SisConnection.SendMessage(evt.Command, evt.Message); 
    }

    void OnApplicationQuit()
    {
        try { SisConnection.fnDisconnect(); }
        catch { }
    }
}