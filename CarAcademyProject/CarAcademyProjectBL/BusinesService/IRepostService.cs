using CarAcademyProjectModels;
using CarAcademyProjectModels.Response;

namespace CarAcademyProjectBL.BusinesService
{
    public interface IRepostService
    {
        Task<IEnumerable<CarService>> GetClientServices(string name);
        Task<IEnumerable<CarService>> GetCarServicesForTime(string start, string end);
    }
}
