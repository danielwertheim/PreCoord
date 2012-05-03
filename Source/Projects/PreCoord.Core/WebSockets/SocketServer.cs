using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using Fleck;
using NCore;
using PreCoord.Core.Serialization;
using PreCoord.Core.WebSockets.Routing;

namespace PreCoord.Core.WebSockets
{
    /// <summary>
    /// Just an simple abstraction over Fleck to bring subscriptions and routing.
    /// </summary>
    public class SocketServer : ISocketServer
    {
        protected IWebSocketServer InternalSocketServer;
        protected List<SocketSubscription> Subscriptions;

        public IRoutes Routes { get; private set; }

        public static SocketServer Start(string location)
        {
            return new SocketServer(new WebSocketServer(location));
        }

        public SocketServer(string location) 
            : this(new WebSocketServer(location))
        {
        }

        protected SocketServer(IWebSocketServer socketServer)
        {
            Ensure.That(socketServer, "socketServer").IsNotNull();

            InternalSocketServer = socketServer;
            Subscriptions = new List<SocketSubscription>();
            Routes = new Routes();
            OnBootstrap();
        }

        public virtual void Dispose()
        {
            if(InternalSocketServer == null)
                throw new ObjectDisposedException(GetType().Name);

            InternalSocketServer.Dispose();
            InternalSocketServer = null;
        }

        protected void OnBootstrap()
        {
            InternalSocketServer.Start(connection =>
            {
                connection.OnOpen = () => OnSubscribe(connection);
                connection.OnClose = () => OnDesubscribe(connection);
                connection.OnMessage = message =>
                {
                    var subscription = GetSubscription(connection);
                    var textMessage = SerializerFor<TextMessage>.Deserialize(message);
                    textMessage.SubscriptionId = subscription.SubscriptionId;

                    OnRecieved(subscription, textMessage);
                };
                connection.OnBinary = message =>
                {
                    var subscription = GetSubscription(connection);
                    var binaryMessage = new BinaryMessage
                    {
                        Data = message, 
                        SubscriptionId = subscription.SubscriptionId
                    };

                    OnRecieved(subscription, binaryMessage);
                };
            });
        }

        protected virtual void OnSubscribe(IWebSocketConnection connection)
        {
            Subscriptions.Add(new SocketSubscription(this, connection));
        }

        protected virtual void OnDesubscribe(IWebSocketConnection connection)
        {
            Parallel.ForEach(Subscriptions.Where(s => s.Connection.Equals(connection)), subscription =>
            {
                if (subscription.OnDesubscribe != null)
                    subscription.OnDesubscribe.Invoke();
            });
        }

        protected virtual void OnRecieved(SocketSubscription subscription, TextMessage message)
        {
            if(message.Action.Equals("Ping", StringComparison.OrdinalIgnoreCase))
            {
                OnPing(subscription, message);
                return;
            }
            Parallel.ForEach(Routes.GetMathingTextMessageRoutes(message.Action, subscription), r => r.Handler(subscription, message));
        }

        protected virtual void OnRecieved(SocketSubscription subscription, BinaryMessage message)
        {
            Parallel.ForEach(Routes.GetMathingBinaryMessageRoutes(subscription), r => r.Handler(subscription, message));
        }

        protected virtual void OnPing(SocketSubscription subscription, TextMessage message)
        {
            subscription.Connection.SendResponse("OnPinged", "Echo: '{0}'.".Inject(message.Data));
        }

        public virtual SocketSubscription GetSubscription(Guid subscriptionId)
        {
            return Subscriptions.SingleOrDefault(s => s.SubscriptionId == subscriptionId);
        }

        public virtual SocketSubscription GetSubscription(IWebSocketConnection connection)
        {
            return Subscriptions.SingleOrDefault(s => s.Connection.Equals(connection));
        }

        public virtual IEnumerable<SocketSubscription> GetSubscriptions()
        {
            return Subscriptions;
        }
    }
}