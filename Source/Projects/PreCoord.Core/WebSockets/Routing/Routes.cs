using System;
using System.Collections.Generic;
using System.Linq;

namespace PreCoord.Core.WebSockets.Routing
{
    public class Routes : IRoutes
    {
        protected readonly IList<TextMessageRoute> TextMessageRoutes;
        protected readonly IList<BinaryMessageRoute> BinaryMessageRoutes;

        public Routes()
        {
            TextMessageRoutes = new List<TextMessageRoute>();
            BinaryMessageRoutes = new List<BinaryMessageRoute>();
        }

        public virtual Routes Register(TextMessageRoute route)
        {
            TextMessageRoutes.Add(route);

            return this;
        }

        public virtual Routes Register(BinaryMessageRoute route)
        {
            BinaryMessageRoutes.Add(route);

            return this;
        }

        public Routes RegisterText(Action<SocketSubscription, TextMessage> handler, TextMessageRoutePredicate predicate = null)
        {
            TextMessageRoutes.Add(new TextMessageRoute(handler, predicate));

            return this;
        }

        public virtual Routes RegisterBinary(Action<SocketSubscription, BinaryMessage> handler, BinaryMessageRoutePredicate predicate = null)
        {
            BinaryMessageRoutes.Add(new BinaryMessageRoute(handler, predicate));

            return this;
        }

        public virtual IEnumerable<TextMessageRoute> GetMathingTextMessageRoutes(string action, SocketSubscription subscription)
        {
            return TextMessageRoutes.Where(r => r.Predicate(subscription, action));
        }

        public virtual IEnumerable<BinaryMessageRoute> GetMathingBinaryMessageRoutes(SocketSubscription subscription)
        {
            return BinaryMessageRoutes.Where(r => r.Predicate(subscription));
        }
    }
}