using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectBL.DataFlowService;
using CarAcademyProjectBL.Services;
using CarAcademyProjectModels.Request;

namespace CarAcademyProject.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IKafkaPublisherService<int, PublishCarServiceRequest>, KafkaPublisherService<int, PublishCarServiceRequest>>();
            services.AddSingleton<ConsumerService<int, PublishCarServiceRequest>>();
            services.AddSingleton<CarServiceDataFlow>();
            return services;
        }
    }
}
