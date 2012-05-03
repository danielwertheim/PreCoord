using System;
using EnsureThat;
using Fleck;

namespace PreCoord.Core.WebSockets
{
    public class SocketSubscription : IEquatable<SocketSubscription>
    {
        public Guid SubscriptionId { get; private set; }
        public ISocketServer Server { get; private set; }
        public IWebSocketConnection Connection { get; private set; }
        public Action OnDesubscribe { get; set; }

        public SocketSubscription(ISocketServer server, IWebSocketConnection connection)
        {
            Ensure.That(server, "server").IsNotNull();
            Ensure.That(connection, "connection").IsNotNull();

            SubscriptionId = Guid.NewGuid();
            Server = server;
            Connection = connection;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SocketSubscription);
        }

        public bool Equals(SocketSubscription other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SubscriptionId.Equals(other.SubscriptionId);
        }

        public override int GetHashCode()
        {
            return SubscriptionId.GetHashCode();
        }
    }
}