using CarAcademyProjectBL.BusinesService;
using CarAcademyProjectBL.DataFlowService;
using CarAcademyProjectBL.HighLevelServices;

namespace CarAcademyProject.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IRepostService, ReportService>();
            services.AddSingleton<CarServiceDataFlow>();
            services.AddSingleton<HighLevelServiceDataFlow>();
            services.AddSingleton<HighLevelRepoService>();
            return services;
        }
    }
}
