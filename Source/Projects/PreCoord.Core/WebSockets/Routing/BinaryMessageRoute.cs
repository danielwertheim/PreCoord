using System;
using EnsureThat;

namespace PreCoord.Core.WebSockets.Routing
{
    public class BinaryMessageRoute
    {
        public readonly Action<SocketSubscription, BinaryMessage> Handler;
        public readonly BinaryMessageRoutePredicate Predicate;

        public BinaryMessageRoute(Action<SocketSubscription, BinaryMessage> handler, BinaryMessageRoutePredicate predicate = null)
        {
            Ensure.That(handler, "handler").IsNotNull();

            Handler = handler;
            Predicate = predicate ?? (subscription => true);
        }
    }
}