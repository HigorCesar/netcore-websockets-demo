using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NetCore.WebSockets.Demo.Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            var httpsv = new HttpServer(4649);
            httpsv.AddWebSocketService<Vehicles>("/Vehicles");
            httpsv.Start();
            if (httpsv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", httpsv.Port);
                foreach (var path in httpsv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();

            httpsv.Stop();
        }
    }
}