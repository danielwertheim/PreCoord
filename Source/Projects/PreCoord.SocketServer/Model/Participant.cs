using System;
using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Model
{
    public abstract class Participant : IEquatable<Participant>
    {
        public readonly SocketSubscription Subscription;

        protected Participant(SocketSubscription subscription)
        {
            Subscription = subscription;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Participant);
        }

        public bool Equals(Participant other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Subscription, other.Subscription);
        }

        public override int GetHashCode()
        {
            return Subscription.GetHashCode();
        }
    }
}