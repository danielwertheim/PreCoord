using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Events
{
    public class JoinedPresentation : IEvent
    {
        public string Name { get; set; }
        public string CurrentSlide { get; set; }
    }
}