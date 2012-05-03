using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreCoord.WebSocketsServer.Model
{
    public class Presentation : IEquatable<Presentation>
    {
        private readonly HashSet<Attendee> _attendies;

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Presenter Presenter { get; private set; }
        public string CurrentSlide { get; private set; }

        public Presentation(string name, Presenter presenter)
        {
            Id = Guid.NewGuid();
            Name = name;
            Presenter = presenter;
            Presenter.OnChangingSlide = newSlide => CurrentSlide = newSlide;

            _attendies = new HashSet<Attendee>();
        }

        public void End()
        {
            var toBeClosedAndRemoved = _attendies.ToArray();
            Parallel.ForEach(toBeClosedAndRemoved, attendee =>
            {
                _attendies.Remove(attendee);
                attendee.Subscription.Connection.Close();
            });
        }

        public void Join(Attendee attendee)
        {
            if (_attendies.Add(attendee))
                attendee.Subscription.OnDesubscribe = () => _attendies.Remove(attendee);
        }

        public IEnumerable<Attendee> GetAttendees()
        {
            return _attendies;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Presentation);
        }

        public bool Equals(Presentation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}