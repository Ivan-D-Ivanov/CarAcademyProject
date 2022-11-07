using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectBL.BusinesService;
using CarAcademyProjectBL.DataFlowService;
using CarAcademyProjectBL.Services;
using CarAcademyProjectModels.Request;

namespace CarAcademyProject.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IRepostService, ReportService>();
            services.AddSingleton<CarServiceDataFlow>();
            return services;
        }
    }
}
