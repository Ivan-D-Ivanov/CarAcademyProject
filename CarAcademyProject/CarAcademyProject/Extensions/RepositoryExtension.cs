using CarAcademyProjectDL.MongoRepo;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectDL.Repositories;

namespace CarAcademyProject.Extensions
{
    public static class RepositoryExtension
    {

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICarRepository, CarRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<ICarServiceRepository, CarServiceRepository>();
            services.AddSingleton<IHighLevelServicesRepository, HighLevelServicesRepository>();

            return services;
        }
    }
}
