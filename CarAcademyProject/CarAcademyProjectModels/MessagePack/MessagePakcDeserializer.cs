using Confluent.Kafka;
using MessagePack;

namespace CarAcademyProjectModels.MessagePack
{
    public class MessagePakcDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<T>(data.ToArray());
        }
    }
}
