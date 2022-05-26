using Assignment_Gui;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF_Client
{
    class WPFClient
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private bool clientRunning;

        private ConcurrentQueue<List<string>> messages;
        private BlockingCollection<char> commands;

        private BroadcastMessage BroadcastMessage;
        private ShowMessage ShowMessage;
        /*private GetMessageToBroadcast GetMessageToBroadcast;
        private DisplayVerse DisplayVerse;*/

        public WPFClient(BroadcastMessage BroadcastMessage)
        {
            tcpClient = new TcpClient();
            messages = new ConcurrentQueue<List<string>>();
            commands = new BlockingCollection<char>();
            this.BroadcastMessage = BroadcastMessage;
            //this.GetMessageToBroadcast = GetMessageToBroadcast;
            this.ShowMessage = ShowMessage;
            //this.DisplayVerse = DisplayVerse;
        }

        public void Run()
        {
            clientRunning = true;
            
            if (Connect(IPAddress.Parse("192.168.0.51"), 4444))
            {
                Task.Run(ReadFromServer);
                Task.Run(DisplayMessages);

                while (clientRunning)
                {  
                    char userInput = commands.Take();

                    string msgToBroadcast = "";
                    if (userInput == 'B')
                    {
                        //msgToBroadcast = GetMessageToBroadcast();
                    }
                    WriteToServer(userInput, msgToBroadcast);
                }
            }
            else
            {
                //ShowMessage("ERROR: Connection to server not successful");
            }
            tcpClient.Close();
        }

        private bool Connect(IPAddress url, int portNumber)
        {
            try
            {
                tcpClient.Connect(IPAddress.Parse("192.168.0.51"), portNumber);
                stream = tcpClient.GetStream();
                reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                //ShowMessage("Exception: " + e.Message);
                return false;
            }
            return true;
        }

        private void WriteToServer(char userChoice, string broadcastMessage)
        {
            if (Char.IsDigit(userChoice)) {
               
                    
            }
            else if (userChoice == 'X')
            {
                clientRunning = false;
            }
            else if (userChoice == 'B' && broadcastMessage.Length > 0)
            {
                writer.WriteLine("" + userChoice);
                writer.WriteLine(broadcastMessage);
                writer.Flush();
            }
            else if (userChoice != 'B')
            {
                writer.WriteLine("" + userChoice);
                writer.Flush();
            }
        }

        private void ReadFromServer()
        {
            while (clientRunning)
            {
                string serverResponse = reader.ReadLine();
                char code = serverResponse.ToUpper()[0];
                List<string> msg = JsonSerializer.Deserialize<List<string>>(serverResponse.Substring(1));
                if (code == 'B')
                {
                    messages.Enqueue(msg);
                }
                else if (code == 'V')
                {
                    messages.Enqueue(msg);
                    //DisplayVerse(msg);
                }
            }
        }

        private void DisplayMessages()
        {
            while (clientRunning)
            {
                List<string> msgList;
                if (messages.TryDequeue(out msgList))
                {
                    BroadcastMessage(msgList);
                }
            }
        }

        public void AddCommand(char command)
        {
            commands.Add(command);
        }
    }
}
