using System;
using System.Collections.Generic;
using System.Linq;
using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Model
{
    //You might want to change this to some nifty persisted store...
    public static class Presentations
    {
        private static readonly HashSet<Presentation> State = new HashSet<Presentation>();

        public static bool AddIfNew(Presentation presentation)
        {
            return State.Add(presentation);
        }

        public static void Remove(Presentation presentation)
        {
            State.Remove(presentation);
        }

        public static Presentation GetPresentationIfPresenter(SocketSubscription subscription)
        {
            return State.SingleOrDefault(p => p.Presenter.Subscription.Equals(subscription));
        }

        public static Presentation GetPresentationByName(string name)
        {
            return State.SingleOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}