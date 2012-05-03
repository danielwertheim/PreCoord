using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Model
{
    public class Attendee : Participant
    {
        public Attendee(SocketSubscription subscription) : base(subscription) {}
    }
}