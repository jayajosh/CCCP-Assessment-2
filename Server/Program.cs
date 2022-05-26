using System;

namespace ServerSide
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server has started!");
            MyServer srvr = new MyServer();

            srvr.Start(); // a synchronous call

            srvr.Stop();

            Console.WriteLine("Server has finished!");
        }
    }
}
