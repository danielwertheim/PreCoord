using System.Collections.Generic;
using System.Threading.Tasks;
using Fleck;
using PreCoord.Core.Serialization;
namespace PreCoord.Core.WebSockets
{
    public static class FleckExtensions
    {
        public static void BroadCastEvent<T>(this ISocketServer server, T @event) where T : class, IEvent
        {
            BroadCastEvent(server.GetSubscriptions(), @event);
        }

        public static void BroadCastEvent<T>(this ISocketServer server, IEnumerable<SocketSubscription> subscriptions, T @event) where T : class, IEvent
        {
            BroadCastEvent(subscriptions, @event);
        }

        private static void BroadCastEvent<T>(IEnumerable<SocketSubscription> subscriptions, T @event) where T : class, IEvent
        {
            Parallel.ForEach(
                subscriptions,
                subscription => subscription.Connection.SendResponse(@event.GetType().Name, SerializerFor<T>.Serialize(@event)));
        }

        public static void SendEvent<T>(this IWebSocketConnection cn, T @event) where T : class, IEvent
        {
            cn.SendResponse(@event.GetType().Name, SerializerFor<T>.Serialize(@event));
        }

        public static void SendResponse(this IWebSocketConnection cn, string name, string data)
        {
            cn.Send(SerializerFor<SocketResponse>.Serialize(new SocketResponse(name, data)));
        }
    }
}