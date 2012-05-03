using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Events
{
    public class PresentationStarted : IEvent
    {
        public string Name { get; set; }
    }
}