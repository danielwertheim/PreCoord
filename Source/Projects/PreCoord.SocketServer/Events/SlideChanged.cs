using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Events
{
    public class SlideChanged : IEvent
    {
        public string NewSlide { get; set; }
    }
}