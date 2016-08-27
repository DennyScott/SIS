// Initial tcp/ip server component taken from MSDN 101 examples, advanced networking in C#
// Modified to display a little more information and to ignore invalid packets
// Most of the server piece is still intact for simple chat
// This is for Proof of Concept ONLY 
// This is to show how to make a simple game server
// At this point, all messages received come across with a CHAT and is parsed

using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace SIS
{
    partial class frmMain : Form
    {

        #region " Windows Form Designer generated code "

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public frmMain()
        {
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            //Add any initialization after the InitializeComponent() call
            // So that we only need to set the title of the application once,
            // we use the AssemblyInfo class (defined in the AssemblyInfo.cs file)
            // to read the AssemblyTitle attribute

            this.mnuAbout.Text = string.Format("&About {0} ...", "temp");
        }

        //Form overrides dispose to clean up the component list.
       

        #endregion

        #region " Standard Menu Code "

        // This code simply shows the About form.
        private void mnuAbout_Click(object sender, System.EventArgs e)
        {
            // Open the About form in Dialog Mode
            //frmAbout frm = new frmAbout();
            //frm.ShowDialog(this);
            //frm.Dispose();
        }

        // This code will close the form.
        private void mnuExit_Click(object sender, System.EventArgs e)
        {
            // Close the current form
            this.Close();
        }

        #endregion

        const int PORT_NUM = 10000;
        private Hashtable clients = new Hashtable();
        private TcpListener listener;
        private Thread listenerThread;

        // This subroutine sends a message to all attached clients
        private void Broadcast(string strMessage)
        {
            UserConnection client;
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.
            foreach (DictionaryEntry entry in clients)
            {
                client = (UserConnection)entry.Value;
                client.SendData(strMessage);
            }
        }

        // This subroutine sends the contents of the Broadcast textbox to all clients, if
        // it is not empty, and clears the textbox
        private void btnBroadcast_Click(object sender, System.EventArgs e)
        {
            if (txtBroadcast.Text != "")
            {
                UpdateStatus("Broadcasting: " + txtBroadcast.Text);
                Broadcast("BROAD|" + txtBroadcast.Text);
                txtBroadcast.Text = string.Empty;
            }
        }

        // This subroutine checks to see if username already exists in the clients 
        // Hashtable.  if it does, send a REFUSE message, otherwise confirm with a JOIN.
        private void ConnectUser(string userName, UserConnection sender)
        {
            if (clients.Contains(userName))
            {
                ReplyToSender("REFUSE", sender);
            }
            else
            {
                sender.Name = userName;
                UpdateStatus(userName + " has joined the chat.");
                clients.Add(userName, sender);
                lstPlayers.Items.Add(sender.Name);
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
            clients.Remove(sender.Name);
            lstPlayers.Items.Remove(sender.Name);
        }

        // This subroutine is used a background listener thread to allow reading incoming
        // messages without lagging the user interface.
        private void DoListen()
        {
            try
            {
                // Listen for new connections.
                listener = new TcpListener(System.Net.IPAddress.Any, PORT_NUM);
                listener.Start();

                do
                {
                    // Create a new user connection using TcpClient returned by
                    // TcpListener.AcceptTcpClient()
                    UserConnection client = new UserConnection(listener.AcceptTcpClient());
                    // Create an event handler to allow the UserConnection to communicate
                    // with the window.
                    client.LineReceived += new LineReceive(OnLineReceived);
                    //AddHandler client.LineReceived, AddressOf OnLineReceived;
                    UpdateStatus("new connection found: waiting for log-in");
                } while (true);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        // When the window closes, stop the listener.
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e) //base.Closing;
        {
            listener.Stop();
        }

        // Start the background listener thread.
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            listenerThread = new Thread(new ThreadStart(DoListen));
            listenerThread.Start();
            UpdateStatus("Listener started");
        }

        // Concatenate all the client names and send them to the user who requested user list
        private void ListUsers(UserConnection sender)
        {
            UserConnection client;
            string strUserList;
            UpdateStatus("Sending " + sender.Name + " a list of users online.");
            strUserList = "LISTUSERS";
            // All entries in the clients Hashtable are UserConnection so it is possible
            // to assign it safely.

            foreach (DictionaryEntry entry in clients)
            {
                client = (UserConnection)entry.Value;
                strUserList = strUserList + "|" + client.Name;
            }

            // Send the list to the sender.
            ReplyToSender(strUserList, sender);
        }

        // This is the event handler for the UserConnection when it receives a full line.
        // Parse the cammand and parameters and take appropriate action.
        private void OnLineReceived(UserConnection sender, string data)
        {
            string[] dataArray;
            // Message parts are divided by "|"  Break the string into an array accordingly.
            // Basically what happens here is that it is possible to get a flood of data during
            // the lock where we have combined commands and overflow
            // to simplify this proble, all I do is split the response by char 13 and then look
            // at the command, if the command is unknown, I consider it a junk message
            // and dump it, otherwise I act on it
            dataArray = data.Split((char)13);
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
                default:
                    // Message is junk do nothing with it.
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
            foreach (DictionaryEntry entry in clients)
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
        private void UpdateStatus(string statusMessage)
        {
            lstStatus.Items.Add(statusMessage);
        }
    }
}