using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using SISConnect.Resources;
using UnityEngine;

namespace SISConnect
{
    // This is the client DLL class code to use for the sockServer
    // include this DLL in your Plugins folder under Assets
    // using it is very simple
    // Look at LinkSyncSCR.cs

    namespace SharpConnect
    {
        public class Connector
        {
            private const int READ_BUFFER_SIZE = 255;
            private const int PORT_NUM = 10000;
            private TcpClient client;
            public ArrayList lstUsers = new ArrayList();
            private Action<SocketMessage> OnCommand;
            private string pUserName;
            private readonly byte[] readBuffer = new byte[READ_BUFFER_SIZE];
            public string res = string.Empty;
            public string strMessage = string.Empty;

            public string Connect(string sNetIp, int iPortNum, string sUserName, Action<SocketMessage> onCommand)
            {
                try
                {
                    pUserName = sUserName;
                    OnCommand = onCommand;
                    // The TcpClient is a subclass of Socket, providing higher level 
                    // functionality like streaming.
                    client = new TcpClient(sNetIp, PORT_NUM);
                    // Start an asynchronous read invoking DoRead to avoid lagging the user
                    // interface.
                    client.GetStream().BeginRead(readBuffer, 0, READ_BUFFER_SIZE, DoRead, null);
                    // Make sure the window is showing before popping up connection dialog.

                    AttemptLogin(sUserName);
                    return "Connection Succeeded";
                }
                catch (Exception ex)
                {
                    return "Server is not active.  Please start server and try again.      " + ex;
                }
            }

            public void AttemptLogin(string user)
            {
                SendData("CONNECT", user);
            }

            public void SendMessage(string command, string message)
            {
                SendData(command, message);
            }

            public void fnDisconnect()
            {
                SendData("DISCONNECT", "");
            }

            public void fnListUsers()
            {
                SendData("REQUESTUSERS", "");
            }

            private void DoRead(IAsyncResult ar)
            {
                try
                {
                    // Finish asynchronous read into readBuffer and return number of bytes read.
                    var BytesRead = client.GetStream().EndRead(ar);
                    if (BytesRead < 1)
                    {
                        // if no bytes were read server has close.  
                        res = "Disconnected";
                        return;
                    }
                    // Convert the byte array the message was saved into, minus two for the
                    // Chr(13) and Chr(10)
                    strMessage = Encoding.ASCII.GetString(readBuffer, 0, BytesRead - 2);
                    ProcessCommands(strMessage);
                    // Start a new asynchronous read into readBuffer.
                    client.GetStream().BeginRead(readBuffer, 0, READ_BUFFER_SIZE, DoRead, null);
                }
                catch
                {
                    res = "Disconnected";
                }
            }

            // Process the command received from the server, and take appropriate action.
            private void ProcessCommands(string strMessage)
            {
                var socketMessage = SocketMessage.CreateFromJson(strMessage);
                OnCommand(socketMessage);
                Debug.Log(socketMessage.Command);
                Debug.Log(socketMessage.Message);
            }

            // Use a StreamWriter to send a message to server.
            private void SendData(string command, string message)
            {
                var time = "temp";
                var socketMessage = new SocketMessage
                {
                    Command = command,
                    Player = pUserName,
                    Message = message,
                    GameTime = time
                };
                Debug.Log("testing here: " + socketMessage.SaveToString());
                var writer = new StreamWriter(client.GetStream());
                writer.Write(socketMessage.SaveToString() + (char) 13);
                writer.Flush();
            }
        }
    }
}