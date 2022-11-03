using System.Data.SqlClient;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.ConfigurationM;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectDL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ILogger<CarRepository> _logger;
        private readonly IOptionsMonitor<ConnectionStrings> _connectionString;

        public CarRepository(ILogger<CarRepository> logger, IConfiguration configuration, IOptionsMonitor<ConnectionStrings> connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public async Task<Car?> AddCar(Car car)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Car>
                        ($"INSERT INTO Cars (PlateNumber,Model,Brand,LastUpdated,MaxSpeed,ClientId) VALUES(@PlateNumber,@Model,@Brand,@LastUpdated,@MaxSpeed,@ClientId)", car))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(AddCar)} : {e}");
            }

            return null;
        }

        public async Task<Car?> DeleteCar(int carId)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();
                    return (await connection.QueryAsync<Car>($"DELETE FROM Cars WHERE Id = @Id", new { Id = carId })).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(DeleteCar)} : {e}");
            }

            return null;
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    var query = $"SELECT * FROM Cars WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var cars = await connection.QueryAsync<Car>(query);
                    return cars;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllCars)} : {e}");
            }

            return Enumerable.Empty<Car>();
        }

        public Task<Car?> GetCarByClientId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Car?> GetCarByPlateNumber(string plateNumber)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    var car = await connection
                        .QueryFirstOrDefaultAsync<Car>
                        ($"SELECT * FROM Cars WITH(NOLOCK) WHERE Cars.PlateNumber LIKE @PlateNumber", new { PlateNumber = plateNumber });
                    return car;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetCarByPlateNumber)} : {e}");
            }

            return null;
        }

        public async Task<Car?> UpdateCar(Car car)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Car>
                        ($"UPDATE Cars SET PlateNumber=@PlateNumber,Brand=@Brand,Model=@Model,MaxSpeed=@MaxSpeed,ClientId=@ClientId,LastUpdated=@LastUpdated WHERE Id = @Id", car))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(UpdateCar)} : {e}");
            }

            return null;
        }
    }
}
