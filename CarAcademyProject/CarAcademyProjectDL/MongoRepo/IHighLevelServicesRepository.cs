using CarAcademyProjectModels;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;

namespace CarAcademyProjectDL.MongoRepo
{
    public interface IHighLevelServicesRepository
    {
        Task<MongoCarService?> SaveCarService(MongoCarService carService);
        Task<MongoCarService?> DeleteCarService(MongoCarService carService);
        Task<IEnumerable<MongoCarService>> GetCarServices(string clientName, string plateNumber);
    }
}
