using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Commands
{
    public class StartOrJoinPresentation : ICommand
    {
        public string Name { get; set; }
    }
}