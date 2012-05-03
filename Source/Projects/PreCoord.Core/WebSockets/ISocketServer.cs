using System;
using System.Collections.Generic;
using Fleck;

namespace PreCoord.Core.WebSockets
{
    public interface ISocketServer : IDisposable
    {
        SocketSubscription GetSubscription(Guid subscriptionId);
        SocketSubscription GetSubscription(IWebSocketConnection connection);
        IEnumerable<SocketSubscription> GetSubscriptions();
    }
}