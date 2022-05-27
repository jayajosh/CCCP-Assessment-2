using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using ui_command;

namespace ServerSide
{
    class ClientService
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

            CommandFactory factory = new CommandFactory();
            factory
                .CreateCommand(UI_Command.INITIALISE_DATABASE)
                .Execute();

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
                    else if (Char.IsDigit(clientMessage[0])) 
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

                    /*                    if (clientMessage.ToUpper().StartsWith("B"))
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
                                        }*/

                    /* if (clientMessage.GetType() == 0.GetType()) {
                          factory
                         .CreateCommand(Int32.Parse(clientMessage))
                         .Execute();
                         switch (Int32.Parse(clientMessage))
                         {
                             case UI_Command.ADD_ITEM_TO_STOCK:
                                 return new AddItemToStockCommand();
                             case UI_Command.ADD_QUANTITY_TO_ITEM:
                                 return new AddQuantityToItemCommand();
                             case UI_Command.TAKE_QUANTITY_FROM_ITEM:
                                 return new TakeQuantityFromItemCommand();
                             case UI_Command.VIEW_INVENTORY_REPORT:
                                 return new ViewInventoryReportCommand();
                             case UI_Command.VIEW_FINANCIAL_REPORT:
                                 return new ViewFinancialReportCommand();
                             case UI_Command.VIEW_TRANSACTION_LOG:
                                 return new ViewTransactionLogCommand();
                             case UI_Command.VIEW_PERSONAL_USAGE_REPORT:
                                 return new ViewPersonalUsageReportCommand();
                             case UI_Command.DISPLAY_MENU:
                                 return DisplayMenuCommand.INSTANCE;
                             case UI_Command.INITIALISE_DATABASE:
                                 return new InitialiseDatabaseCommand();
                             default:
                                 return new NullCommand();
                         }
                     }
                     clientMessage = reader.ReadLine();*/
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
    }
}
