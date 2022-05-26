//using Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

namespace ServerSide
{
    /*class ClientService
    {

        private Socket socket;
        private NetworkStream stream;
        public StreamReader reader { get; private set; }
        public StreamWriter writer { get; private set; }

        private RemoveClient removeMe;

        private BroadcastMessageToAllClients broadcast;

        public ClientService(Socket socket, RemoveClient rc, BroadcastMessageToAllClients broadcast)
        {
            this.socket = socket;
            removeMe = rc;
            this.broadcast = broadcast;

            stream = new NetworkStream(socket);
            reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
        }
        public void InteractWithClient()
        {
            try
            {
                string clientMessage = reader.ReadLine();
                while (clientMessage != null)
                {
                    if (clientMessage.ToUpper().StartsWith("B"))
                    {
                        clientMessage = reader.ReadLine();
                        broadcast(
                            string.Format(
                                "{0}: {1}", 
                                socket.RemoteEndPoint.ToString(), 
                                clientMessage));
                    }
                    else
                    {
                        string response = 
                            new CommandFactory()
                                .Create(clientMessage.ToUpper()[0])
                                .Execute();

                        lock (writer)
                        {
                            writer.WriteLine(response);
                            writer.Flush();
                        }
                    }

                    clientMessage = reader.ReadLine();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }

            Console.WriteLine("Goodbye from " + socket.RemoteEndPoint.ToString());
            Close();
        }

        public void Close()
        {
            try
            {
                removeMe(this);
                socket.Shutdown(SocketShutdown.Both);
            }
            finally
            {
                socket.Close();
            }
        }

        public void BroadCastMessage(string message)
        {
            lock (writer)
            {
                writer.WriteLine(message);
                writer.Flush();
            }
        }
    }*/
}
