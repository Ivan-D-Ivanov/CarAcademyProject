using System.Data.SqlClient;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.ConfigurationM;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectDL.Repositories
{
    public class CarServiceRepository : ICarServiceRepository
    {
        private readonly ILogger<CarServiceRepository> _logger;
        private readonly IOptionsMonitor<ConnectionStrings> _connectionString;

        public CarServiceRepository(ILogger<CarServiceRepository> logger, IOptionsMonitor<ConnectionStrings> connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public async Task<CarService?> AddCarService(CarService carService)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<CarService>
                        ($"INSERT INTO CarServices (ClientId,CarId,StartDate,EndDate,MDifficult,ManipulationDescription) VALUES(@ClientId,@CarId,@StartDate,@EndDate,@MDifficult,@ManipulationDescription)", carService))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(AddCarService)} : {e}");
            }

            return null;
        }

        public async Task<IEnumerable<CarService>> GetAllCarServices()
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    var query = $"SELECT * FROM CarServices WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var cars = await connection.QueryAsync<CarService>(query);
                    return cars;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllCarServices)} : {e}");
            }

            return Enumerable.Empty<CarService>();
        }
    }
}
