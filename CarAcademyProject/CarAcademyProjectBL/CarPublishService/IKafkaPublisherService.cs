namespace CarAcademyProject.CarAcademyProjectBL.CarPublishService
{
    public interface IKafkaPublisherService<Tkey, TValue>
    {
        Task PublishTopic(Tkey key, TValue person);
    }
}
