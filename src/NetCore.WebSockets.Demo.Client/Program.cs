using System;
using System.Threading;
using WebSocketSharp;

namespace NetCore.WebSockets.Demo.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            using var nf = new Notifier();
            using var ws = new WebSocket("ws://localhost:4649/Vehicles");

            ws.OnMessage += (sender, e) =>
                nf.Notify(
                    new NotificationMessage
                    {
                        Body = !e.IsPing ? e.Data : "Received a ping."
                    }
                );

            ws.OnError += (sender, e) =>
                nf.Notify(
                    new NotificationMessage
                    {
                        Summary = "WebSocket Error", Body = e.Message, Icon = "notification-message-im"
                    }
                );

            ws.OnClose += (sender, e) =>
                nf.Notify(
                    new NotificationMessage
                    {
                        Summary = String.Format("WebSocket Close ({0})", e.Code), Body = e.Reason, Icon = "notification-message-im"
                    }
                );
            ws.Log.Level = LogLevel.Trace;
            ws.Connect();

            Console.WriteLine("\nType 'exit' to exit.\n");
            while (true)
            {
                Thread.Sleep(1000);
                Console.Write("> ");
                var msg = Console.ReadLine();
                if (msg == "exit")
                    break;

                // Send a text message.
                ws.Send(msg);
            }
        }
    }
}