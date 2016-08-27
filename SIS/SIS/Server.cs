using System;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace SIS
{
    public class Server
    {
        private const int PortNum = 10000;
        private readonly Hashtable _clients = new Hashtable();
        private TcpListener _listener;
        private Thread _listenerThread;

        // This subroutine sends a message to all attached clients
        private void Broadcast(string strMessage)
        {
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.
            foreach (DictionaryEntry entry in _clients)
            {
                var client = (UserConnection)entry.Value;
                client.SendData(strMessage);
            }
        }

        // This subroutine checks to see if username already exists in the clients 
        // Hashtable.  if it does, send a REFUSE message, otherwise confirm with a JOIN.
        private void ConnectUser(string userName, UserConnection sender)
        {
            if (_clients.Contains(userName))
            {
                ReplyToSender("REFUSE", sender);
            }
            else
            {
                sender.Name = userName;
                UpdateStatus(userName + " has joined the chat.");
                _clients.Add(userName, sender);
                // Send a JOIN to sender, and notify all other clients that sender joined
                ReplyToSender("JOIN", sender);
                SendToClients("CHAT|" + sender.Name + " has joined the chat.", sender);
            }
        }

        // This subroutine notifies other clients that sender left the chat, and removes
        // the name from the clients Hashtable
        private void DisconnectUser(UserConnection sender)
        {
            UpdateStatus(sender.Name + " has left the chat.");
            SendToClients("CHAT|" + sender.Name + " has left the chat.", sender);
            _clients.Remove(sender.Name);
        }

        // This subroutine is used a background listener thread to allow reading incoming
        // messages without lagging the user interface.
        private void DoListen()
        {
            try
            {
                // Listen for new connections.
                _listener = new TcpListener(System.Net.IPAddress.Any, PortNum);
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
            var strUserList = (from DictionaryEntry entry in _clients select (UserConnection) entry.Value).Aggregate("LISTUSERS", (current, client) => current + "|" + client.Name);
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.

            // Send the list to the sender.
            ReplyToSender(strUserList, sender);
        }

        // This is the event handler for the UserConnection when it receives a full line.
        // Parse the cammand and parameters and take appropriate action.
        private void OnLineReceived(UserConnection sender, string data)
        {
            // Message parts are divided by "|"  Break the string into an array accordingly.
            // Basically what happens here is that it is possible to get a flood of data during
            // the lock where we have combined commands and overflow
            // to simplify this proble, all I do is split the response by char 13 and then look
            // at the command, if the command is unknown, I consider it a junk message
            // and dump it, otherwise I act on it
            var dataArray = data.Split((char)13);
            dataArray = dataArray[0].Split((char)124);

            // dataArray(0) is the command.
            switch (dataArray[0])
            {
                case "CONNECT":
                    ConnectUser(dataArray[1], sender);
                    break;
                case "CHAT":
                    SendChat(dataArray[1], sender);
                    break;
                case "DISCONNECT":
                    DisconnectUser(sender);
                    break;
                case "REQUESTUSERS":
                    ListUsers(sender);
                    break;
            }
        }

        // This subroutine sends a response to the sender.
        private void ReplyToSender(string strMessage, UserConnection sender)
        {
            sender.SendData(strMessage);
        }

        // Send a chat message to all clients except sender.
        private void SendChat(string message, UserConnection sender)
        {
            UpdateStatus(sender.Name + ": " + message);
            SendToClients("CHAT|" + sender.Name + ": " + message, sender);
        }

        // This subroutine sends a message to all attached clients except the sender.
        private void SendToClients(string strMessage, UserConnection sender)
        {
            UserConnection client;
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.
            foreach (DictionaryEntry entry in _clients)
            {
                client = (UserConnection)entry.Value;
                // Exclude the sender.
                if (client.Name != sender.Name)
                {
                    client.SendData(strMessage);
                }
            }
        }

        // This subroutine adds line to the Status listbox
        private static void UpdateStatus(string statusMessage)
        {
            Console.WriteLine(statusMessage);
        }
    }
}