using System;
using ServiceStack.Text;

namespace PreCoord.Core.Serialization
{
    public static class SerializerFor<T> where T : class
    {
        private static readonly JsonSerializer<T> InnerSerializer;

        public static Func<T, string> Serialize;
        public static Func<string, T> Deserialize;

        static SerializerFor()
        {
            InnerSerializer = new JsonSerializer<T>();
            Reset();
        }

        static void Reset()
        {
            Serialize = InnerSerializer.SerializeToString;
            Deserialize = InnerSerializer.DeserializeFromString;
        }
    }
}