using CarAcademyProjectModels;

namespace CarAcademyProjectDL.RepoInterfaces
{
    public interface ICarServiceRepository
    {
        Task<CarService?> AddCarService(CarService carService);
        Task<IEnumerable<CarService>> GetAllCarServices();
    }
}
