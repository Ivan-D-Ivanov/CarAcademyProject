using CarAcademyProjectModels;

namespace CarAcademyProjectDL.RepoInterfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car?> AddCar(Car car);
        Task<Car?> DeleteCar(int carId);
        Task<Car?> GetCarByPlateNumber(string plateNumber);
        Task<Car?> UpdateCar(Car car);
        Task<Car?> GetCarByClientId(int id);
    }
}
