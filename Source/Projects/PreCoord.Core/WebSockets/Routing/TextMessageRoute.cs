using System;
using EnsureThat;

namespace PreCoord.Core.WebSockets.Routing
{
    public class TextMessageRoute
    {
        public readonly Action<SocketSubscription, TextMessage> Handler;
        public readonly TextMessageRoutePredicate Predicate;

        public TextMessageRoute(Action<SocketSubscription, TextMessage> handler, TextMessageRoutePredicate predicate = null)
        {
            Ensure.That(handler, "handler").IsNotNull();

            Handler = handler;
            Predicate = predicate ?? ((action, subscription) => true);
        }
    }
}