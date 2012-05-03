using System;
using PreCoord.Core.WebSockets;

namespace PreCoord.WebSocketsServer.Commands
{
    public class ChangeSlide : ICommand
    {
        public string NewSlide { get; set; }
    }
}