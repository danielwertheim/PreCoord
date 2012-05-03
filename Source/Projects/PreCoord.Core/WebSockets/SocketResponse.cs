using System;
using EnsureThat;

namespace PreCoord.Core.WebSockets
{
    [Serializable]
    public class SocketResponse
    {
        public string Name { get; private set; }
        public string Data { get; private set; }

        public SocketResponse(string name, string data)
        {
            Ensure.That(name, "name").IsNotNullOrWhiteSpace();

            Name = name.ToLower();
            Data = data;
        }
    }
}