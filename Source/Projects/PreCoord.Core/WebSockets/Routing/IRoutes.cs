using System;
using System.Collections.Generic;

namespace PreCoord.Core.WebSockets.Routing
{
    public interface IRoutes 
    {
        Routes Register(TextMessageRoute route);
        Routes Register(BinaryMessageRoute route);
        Routes RegisterText(Action<SocketSubscription, TextMessage> handler, TextMessageRoutePredicate predicate = null);
        Routes RegisterBinary(Action<SocketSubscription, BinaryMessage> handler, BinaryMessageRoutePredicate predicate = null);

        IEnumerable<TextMessageRoute> GetMathingTextMessageRoutes(string action, SocketSubscription subscription);
        IEnumerable<BinaryMessageRoute> GetMathingBinaryMessageRoutes(SocketSubscription subscription);
    }
}