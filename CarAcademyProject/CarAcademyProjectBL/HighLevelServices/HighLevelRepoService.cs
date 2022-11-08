using AutoMapper;
using CarAcademyProjectDL.MongoRepo;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarAcademyProjectBL.HighLevelServices
{
    public class HighLevelRepoService
    {
        private readonly ILogger<HighLevelRepoService> _logger;
        private readonly IHighLevelServicesRepository _highLevelServicesRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HighLevelRepoService(IHighLevelServicesRepository highLevelServicesRepository, IMapper mapper, IMediator mediator, ILogger<HighLevelRepoService> logger)
        {
            _highLevelServicesRepository = highLevelServicesRepository;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IEnumerable<MongoCarService?>> DeleteCarService(string clientName, string plateNumber)
        {
            var carService = _highLevelServicesRepository.GetCarServices(clientName, plateNumber);
            if(carService.Result.Count() == 0) return null;

            var result = new List<MongoCarService?>();
            foreach (var item in carService.Result)
            {
                result.Add(await _highLevelServicesRepository.DeleteCarService(item));
            }

            return result;
        }

        public async Task<IEnumerable<MongoCarService>> GetCarServices(string clientName, string plateNumber)
        {
            var result = await _highLevelServicesRepository.GetCarServices(clientName, plateNumber);
            return result;
        }

        public async Task<MongoCarService> SaveCarService(MongoCarService carService)
        {
            var result = await _highLevelServicesRepository.SaveCarService(carService);
            return result;
        }

        public async Task<IEnumerable<MongoCarService>> FinishHighLevelCarService(string clientName, string plateNumber)
        {
            var carService = _highLevelServicesRepository.GetCarServices(clientName, plateNumber);
            if (carService.Result.Count() == 0) return null;

            foreach (var item in carService.Result)
            {
                var mappedResult = _mapper.Map<CarServiceRquest>(item);
                await _mediator.Send(new AddCarServiceCommand(mappedResult));
                await _highLevelServicesRepository.DeleteCarService(item);
            }

            return carService.Result;
        }
    }
}
