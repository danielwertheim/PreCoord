using System.Linq;
using PreCoord.Core.WebSockets;
using PreCoord.WebSocketsServer.Commands;
using PreCoord.WebSocketsServer.Events;
using PreCoord.WebSocketsServer.Model;

namespace PreCoord.WebSocketsServer.Controllers
{
    public class PresentationController
    {
        public virtual void StartOrJoinPresentation(SocketSubscription sender, TextMessage message)
        {
            var cmd = message.ToCommand<StartOrJoinPresentation>();

            var presentation = Presentations.GetPresentationByName(cmd.Name);
            if(presentation == null)
                OnStartPresentation(cmd, sender);
            else
                OnJoinPresentation(presentation, sender);                
        }

        protected virtual void OnStartPresentation(StartOrJoinPresentation cmd, SocketSubscription sender)
        {
            var presentation = new Presentation(cmd.Name, new Presenter(sender));
            
            if (Presentations.AddIfNew(presentation))
            {
                presentation.Presenter.Subscription.OnDesubscribe = () =>
                {
                    Presentations.Remove(presentation);
                    presentation.End();
                };

                sender.Connection.SendEvent(new PresentationStarted
                {
                    Name = presentation.Name
                });
            }
        }

        protected virtual void OnJoinPresentation(Presentation presentation, SocketSubscription sender)
        {
            presentation.Join(new Attendee(sender));
            sender.Connection.SendEvent(new JoinedPresentation
            {
                Name = presentation.Name,
                CurrentSlide = presentation.CurrentSlide
            });
        }

        public virtual void ChangeSlide(SocketSubscription sender, TextMessage message)
        {
            var cmd = message.ToCommand<ChangeSlide>();
            var presentation = Presentations.GetPresentationIfPresenter(sender);
            if (presentation == null)
                return;

            if(presentation.Presenter.ChangeSlide(cmd.NewSlide))
                sender.Server.BroadCastEvent(
                    presentation.GetAttendees().Select(a => a.Subscription),
                    new SlideChanged
                    {
                        NewSlide = cmd.NewSlide
                    });
        }
    }
}