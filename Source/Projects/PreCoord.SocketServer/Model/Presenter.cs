using System;
using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Model
{
    public class Presenter : Participant
    {
        public Action<string> OnChangingSlide;

        public Presenter(SocketSubscription subscription) : base(subscription) {}

        public bool ChangeSlide(string newSlide)
        {
            if(string.IsNullOrWhiteSpace(newSlide))
                return false;
            
            if(OnChangingSlide != null)
                OnChangingSlide.Invoke(newSlide);

            return true;
        }
    }
}