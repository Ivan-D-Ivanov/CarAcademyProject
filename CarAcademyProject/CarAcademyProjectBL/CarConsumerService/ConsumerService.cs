using CarAcademyProjectModels.ConfigurationM;
using CarAcademyProjectModels.MessagePack;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectBL.Services
{
    public class ConsumerService<TKey, TValue>
    {
        private readonly IOptionsMonitor<IOptionsSettings> _subSettings;
        private IConsumer<TKey, TValue> _consumer;
        private ConsumerConfig _consumerConfig;

        public ConsumerService(IOptionsMonitor<IOptionsSettings> subSettings)
        {
            _subSettings = subSettings;
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _subSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset)_subSettings.CurrentValue.AutoOffsetReset,
                GroupId = _subSettings.CurrentValue.GroupId,
            };
            _consumer = new ConsumerBuilder<TKey, TValue>(_consumerConfig).SetKeyDeserializer(new MessagePakcDeserializer<TKey>())
                .SetValueDeserializer(new MessagePakcDeserializer<TValue>()).Build();
            _consumer.Subscribe(_subSettings.CurrentValue.TopicName);
        }

        public ConsumeResult<TKey, TValue> Consume()
        {
            var cr = _consumer.Consume();
            return cr;
        }
    }
}
