using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using SIS.Resources;

namespace SIS
{
    public class Server
    {
        private const int PortNum = 10000;
        private readonly Hashtable _clients = new Hashtable();
        private TcpListener _listener;
        private Thread _listenerThread;

        // This subroutine sends a message to all attached clients
        private void Broadcast(SocketMessage socketMessage)
        {
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.
            foreach (DictionaryEntry entry in _clients)
            {
                var client = (UserConnection) entry.Value;
                client.SendData(socketMessage);
            }
        }

        // This subroutine checks to see if username already exists in the clients 
        // Hashtable.  if it does, send a REFUSE message, otherwise confirm with a JOIN.
        private void ConnectUser(string userName, UserConnection sender)
        {
            Console.WriteLine("Trying to connect");
            if (_clients.Contains(userName))
            {
                ReplyToSender("REFUSE", "Connection failed", sender);
            }
            else
            {
                var message = userName + " has joined the chat.";
                sender.Name = userName;
                UpdateStatus(message);
                _clients.Add(userName, sender);
                // Send a JOIN to sender, and notify all other clients that sender joined
                ReplyToSender("JOIN", message, sender);
                var socketMessage = new SocketMessage
                {
                    Message = sender.Name + " has joined the chat.",
                    Player = sender.Name,
                    Command = "CHAT",
                    GameTime = "temp"
                };
                SendToClients(socketMessage);
            }
        }

        // This subroutine notifies other clients that sender left the chat, and removes
        // the name from the clients Hashtable
        private void DisconnectUser(UserConnection sender)
        {
            UpdateStatus(sender.Name + " has left the chat.");
            var socketMessage = new SocketMessage
            {
                Message = sender.Name + " has left the chat.",
                Player = sender.Name,
                Command = "CHAT",
                GameTime = "temp"
            };
            SendToClients(socketMessage);
            _clients.Remove(sender.Name);
        }

        // This subroutine is used a background listener thread to allow reading incoming
        // messages without lagging the user interface.
        private void DoListen()
        {
            try
            {
                // Listen for new connections.
                _listener = new TcpListener(IPAddress.Any, PortNum);
                _listener.Start();

                do
                {
                    // Create a new user connection using TcpClient returned by
                    // TcpListener.AcceptTcpClient()
                    var client = new UserConnection(_listener.AcceptTcpClient());
                    // Create an event handler to allow the UserConnection to communicate
                    // with the window.
                    client.LineReceived += OnLineReceived;
                    //AddHandler client.LineReceived, AddressOf OnLineReceived;
                    UpdateStatus("new connection found: waiting for log-in");
                } while (true);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        // When the window closes, stop the listener.
        private void Closing() //base.Closing;
        {
            _listener.Stop();
        }

        // Start the background listener thread.
        public void Load()
        {
            _listenerThread = new Thread(DoListen);
            _listenerThread.Start();
            UpdateStatus("Listener started");
        }

        // Concatenate all the client names and send them to the user who requested user list
        private void ListUsers(UserConnection sender)
        {
            UpdateStatus("Sending " + sender.Name + " a list of users online.");
            var strUserList =
                (from DictionaryEntry entry in _clients select (UserConnection) entry.Value).Aggregate("LISTUSERS",
                    (current, client) => current + "|" + client.Name);
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.

            // Send the list to the sender.
            ReplyToSender("USERLIST", strUserList, sender);
        }

        // This is the event handler for the UserConnection when it receives a full line.
        // Parse the cammand and parameters and take appropriate action.
        private void OnLineReceived(UserConnection sender, string data)
        {
            Console.WriteLine("Line Recieved");
            // Message parts are divided by "|"  Break the string into an array accordingly.
            // Basically what happens here is that it is possible to get a flood of data during
            // the lock where we have combined commands and overflow
            // to simplify this proble, all I do is split the response by char 13 and then look
            // at the command, if the command is unknown, I consider it a junk message
            // and dump it, otherwise I act on it
            var dataArray = data.Split((char) 13);
            Console.WriteLine(dataArray[0]);
            var socketMessage = JsonConvert.DeserializeObject<SocketMessage>(data);
            
            Console.WriteLine(socketMessage.Command);

            // dataArray(0) is the command.
            switch (socketMessage.Command)
            {
                case "CONNECT":
                    ConnectUser(socketMessage.Player, sender);
                    break;
                case "BROADCAST":
                    Broadcast(socketMessage);
                    break;
                case "DISCONNECT":
                    DisconnectUser(sender);
                    break;
                case "REQUESTUSERS":
                    ListUsers(sender);
                    break;
                default:
                    SendToClients(socketMessage);
                    break;
            }
        }

        // This subroutine sends a response to the sender.
        private void ReplyToSender(string command, string message, UserConnection sender)
        {
            var gameTime = "temp";
            sender.SendData(new SocketMessage
            {
                Command = command,
                Message = message,
                Player = sender.Name,
                GameTime = gameTime
            });
        }


        // This subroutine sends a message to all attached clients except the sender.
        private void SendToClients(SocketMessage socketMessage)
        {
            var gameTime = "temp";
            UpdateStatus(socketMessage.Message);
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.
            foreach (DictionaryEntry entry in _clients)
            {
                var client = (UserConnection) entry.Value;
                // Exclude the sender.
                if (client.Name != socketMessage.Player)
                    client.SendData(socketMessage);
            }
        }

        // This subroutine adds line to the Status listbox
        private static void UpdateStatus(string statusMessage)
        {
            Console.WriteLine(statusMessage);
        }
    }
}