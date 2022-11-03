using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectModels;

namespace CarAcademyProject.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IKafkaPublisherService<int, CarService>, KafkaPublisherService<int, CarService>>();
            return services;
        }
    }
}
