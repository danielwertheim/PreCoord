using PreCoord.Core.Serialization;

namespace PreCoord.Core.WebSockets
{
    public static class TextMessageExtensions
    {
        public static T ToCommand<T>(this TextMessage message) where T : class, ICommand
        {
            return SerializerFor<T>.Deserialize(message.Data);
        }
    }
}