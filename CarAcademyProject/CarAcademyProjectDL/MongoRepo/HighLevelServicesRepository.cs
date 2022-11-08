using CarAcademyProjectModels.ConfigurationM;
using CarAcademyProjectModels.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarAcademyProjectDL.MongoRepo
{
    public class HighLevelServicesRepository : IHighLevelServicesRepository
    {
        private readonly ILogger<HighLevelServicesRepository> _logger;
        private readonly IOptionsMonitor<MongoDbConfigurator> _optionsMonitor;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<MongoCarService> _mongoCollection;

        public HighLevelServicesRepository(ILogger<HighLevelServicesRepository> logger, IOptionsMonitor<MongoDbConfigurator> optionsMonitor)
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
            _mongoClient = new MongoClient(_optionsMonitor.CurrentValue.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_optionsMonitor.CurrentValue.DatabaseName);
            _mongoCollection = _mongoDatabase.GetCollection<MongoCarService>(_optionsMonitor.CurrentValue.CollectionCarServiceRquest);

        }

        public async Task<MongoCarService?> DeleteCarService(MongoCarService carService)
        {
            try
            { 
                await _mongoCollection.DeleteOneAsync(x => x.ClientName == carService.ClientName);
                return carService;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<MongoCarService>> GetCarServices(string clientName, string plateNumber)
        {
            try
            {
                var result = await _mongoCollection.FindAsync(x => x.ClientName == clientName && x.CarPlateNumber == plateNumber);
                return await result.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<MongoCarService?> SaveCarService(MongoCarService carService)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(carService);
                return carService;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
