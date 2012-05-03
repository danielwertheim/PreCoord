using System;
using PreCoord.Core.Consoles;
using PreCoord.Core.WebSockets;
using PreCoord.WebSocketsServer.Controllers;

namespace PreCoord.WebSocketsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new SocketServer("ws://localhost:7777"))
            {
                //Not prefered, you would prabably resolve via IoC, but we
                //just need a controller to handle our incoming commands
                var controller = new PresentationController();

                //You can see the below as setting up a route simliar to an
                //Asp.Net MVC Controller-Action. The second argument is a
                //predicate
                server.Routes
                    .RegisterText(controller.StartOrJoinPresentation, 
                        (subscription, action) => action.Equals("StartOrJoinPresentation", StringComparison.OrdinalIgnoreCase))
                    .RegisterText(controller.ChangeSlide,
                        (subscription, action) => action.Equals("ChangeSlide", StringComparison.OrdinalIgnoreCase));

                Console.WriteLine("WebSocketServer, Socket server is up.");
                ExitConsole.WhenCtrlC();
            }
        }
    }
}
