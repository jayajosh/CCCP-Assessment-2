
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ui_command;

namespace ServerSide
{
    delegate void RemoveClient(ClientService c);
    delegate void BroadcastMessageToAllClients(string message);

    class MyServer
    {
        private TcpListener tcpListener;
        private List<ClientService> clientServices;

        public MyServer()
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.0.51");
            tcpListener = new TcpListener(ipAddress, 4444);
            clientServices = new List<ClientService>();
            Console.WriteLine("Listening....");
        }

        public void Start()
        {
            tcpListener.Start();

            // A timer to trigger broadcasting.
            // The first interval is 3 seconds, 
            // subsequent intervals are 5 seconds
            Timer t = new Timer(BroadcastTime);
            t.Change(3000, 5000); 

            while (true)
            {
                Socket s = tcpListener.AcceptSocket();  //blocks until a connection is made
                ClientService clientService = new ClientService(s, RemoveClient, BroadcastMessage);
                clientServices.Add(clientService);
                Task.Run(clientService.InteractWithClient);
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void RemoveClient(ClientService c)
        {
            //Console.WriteLine("REMOVING CLIENT");
            clientServices.Remove(c);
        }

        private void BroadcastTime(object state)
        {
            string msg = string.Format(
                        "{0}: The time is {1}",
                        tcpListener.LocalEndpoint.ToString(),
                        DateTime.Now.ToString("HH:mm:ss"));

            BroadcastMessage(msg);
        }

        private void BroadcastMessage(string msg)
        {
            if (clientServices.Count > 0)
            {
                Console.WriteLine("BROADCASTING: " + msg);
                string msgToBroadcast =
                    new CommandFactory()
                        .Create('B', msg)
                        .Execute();

                clientServices.ForEach(c => c.BroadCastMessage(msgToBroadcast));
            }
            else
            {
                Console.WriteLine("NOT BROADCASTING BECAUSE NO CLIENTS");
            }
        }
    }
}
