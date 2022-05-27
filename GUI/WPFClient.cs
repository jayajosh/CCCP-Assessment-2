using Assignment_Gui;
using AssignmentMain.Objects;
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
        private List<string> returnData;
        private BlockingCollection<char> commands;

        private BroadcastMessage BroadcastMessage;
        private GetDataToBroadcast GetDataToBroadcast;
        private ShowMessage ShowMessage;
        private ReturnData ReturnData;
        /*private GetMessageToBroadcast GetMessageToBroadcast;
        private DisplayVerse DisplayVerse;*/

        public WPFClient(BroadcastMessage BroadcastMessage, GetDataToBroadcast GetDataToBroadcast, ReturnData ReturnData)
        {
            tcpClient = new TcpClient();
            messages = new ConcurrentQueue<List<string>>();
            commands = new BlockingCollection<char>();
            this.BroadcastMessage = BroadcastMessage;
            this.GetDataToBroadcast = GetDataToBroadcast;
            this.ShowMessage = ShowMessage;
            this.ReturnData = ReturnData;
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

                    Dictionary<string, dynamic> msgToBroadcast;
                    if (Char.IsDigit(userInput))
                    {
                        msgToBroadcast = GetDataToBroadcast();
                        WriteToServer(userInput, msgToBroadcast);
                    }
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

        private void WriteToServer(char userChoice, Dictionary<string, dynamic> broadcastData)
        {
            if (Char.IsDigit(userChoice)) {
                writer.WriteLine("" + userChoice);
                writer.WriteLine(JsonSerializer.Serialize(broadcastData));
               /* foreach (string line in broadcastData)
                {
                    writer.WriteLine(line);
                }*/
                writer.Flush();
            }
            else if (userChoice == 'X')
            {
                clientRunning = false;
            }
            /*else if (userChoice == 'B' && broadcastData.Length > 0)
            {
                writer.WriteLine("" + userChoice);
                writer.WriteLine(broadcastData);
                writer.Flush();
            }
            else if (userChoice != 'B')
            {
                writer.WriteLine("" + userChoice);
                writer.Flush();
            }*/
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
                if (Char.IsDigit(code))
                {
                    ReturnData(code, msg);
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
