using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.Response;
using Microsoft.Extensions.Logging;

namespace CarAcademyProjectBL.BusinesService
{
    public class ReportService : IRepostService
    {
        private readonly ICarServiceRepository _carServiceRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ReportService> _logger;

        public ReportService(ICarServiceRepository carServiceRepository, ILogger<ReportService> logger, IClientRepository clientRepository)
        {
            _carServiceRepository = carServiceRepository;
            _logger = logger;
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<CarService>> GetCarServicesForTime(string start, string end)
        {
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            var result = _carServiceRepository.GetAllCarServices().Result.Where(x => x.StartDate >= startDate && x.EndDate <= endDate);

            _logger.LogInformation($"There is {result.Count()} CarServices from : {startDate} to : {endDate}");
            return result;
        }

        public async Task<IEnumerable<CarService>> GetClientServices(string name)
        {
            var client = await _clientRepository.GetClientByName(name);
            if (client == null) return Enumerable.Empty<CarService>();
            var result = _carServiceRepository.GetAllCarServices().Result.Where(x => x.ClientId == client.Id);

            _logger.LogInformation($"There is {result.Count()} CarServices for Client : {client.FirstName} {client.LastName}");
            return result;
        }
    }
}
