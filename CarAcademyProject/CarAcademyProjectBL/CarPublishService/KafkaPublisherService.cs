using CarAcademyProjectModels.ConfigurationM;
using CarAcademyProjectModels.MessagePack;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CarAcademyProject.CarAcademyProjectBL.CarPublishService
{
    public class KafkaPublisherService<TKey, TValue> : IKafkaPublisherService<TKey, TValue>
    {
        private readonly IOptionsMonitor<KafkaPublisherSettings> _optionsMonitor;
        private ProducerConfig _producerConfig;

        public KafkaPublisherService(IOptionsMonitor<KafkaPublisherSettings> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
            _producerConfig = new ProducerConfig() { BootstrapServers = _optionsMonitor.CurrentValue.BootstrapServers };
        }

        public async Task PublishTopic(TKey key,TValue item)
        {
            var producer = new ProducerBuilder<TKey, TValue>(_producerConfig)
                .SetKeySerializer(new MessagePackSerializer<TKey>())
                .SetValueSerializer(new MessagePackSerializer<TValue>())
                .Build();

            try
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = key,
                    Value = item
                };

                var result = await producer.ProduceAsync(_optionsMonitor.CurrentValue.TopicName, msg);
                if (result == null) Console.WriteLine($"Deliver : {result.Value} to {result.TopicPartitionOffset}");
            }
            catch (ProduceException<TKey, TValue> e)
            {
                Console.WriteLine($"Delivery : {e.Error.Reason}");
            }
        }
    }
}
