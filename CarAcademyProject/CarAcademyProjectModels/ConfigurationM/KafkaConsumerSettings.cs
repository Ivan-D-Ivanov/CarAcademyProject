namespace CarAcademyProjectModels.ConfigurationM
{
    public class KafkaConsumerSettings : IOptionsSettings
    {
        public string BootstrapServers { get; set; }

        public int AutoOffsetReset { get; set; }

        public string GroupId { get; set; }

        public string TopicName { get; set; }
    }
}
