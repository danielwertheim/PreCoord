using System;
using EnsureThat;

namespace PreCoord.Core.WebSockets
{
    [Serializable]
    public class TextMessage
    {
        private string _action;
        private string _data;

        public Guid SubscriptionId { get; set; }
        public string Action
        {
            get { return _action; }
            set
            {
                Ensure.That(value, "Action").IsNotNullOrEmpty();
                _action = value.ToLower();
            }
        }
        public string Data
        {
            get { return _data; }
            set { _data = value ?? string.Empty; }
        }
    }
}