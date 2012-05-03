using System;

namespace PreCoord.Core.WebSockets
{
    [Serializable]
    public class BinaryMessage
    {
        private byte[] _data;

        public Guid SubscriptionId { get; set; }
        public byte[] Data
        {
            get { return _data; }
            set { _data = value ?? new byte[0]; }
        }
    }
}