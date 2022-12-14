using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.CarServiceHandler
{
    public class AddCarServiceCommandHandler : IRequestHandler<AddCarServiceCommand, CarServiceResponse>
    {
        private readonly ICarServiceRepository _carServiceRepository;
        private readonly ICarRepository _carRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<AddCarServiceCommandHandler> _logger;

        public AddCarServiceCommandHandler(
            ICarServiceRepository carServiceRepository,
            ICarRepository carRepository, 
            IClientRepository clientRepository, 
            ILogger<AddCarServiceCommandHandler> logger)
        {
            _carServiceRepository = carServiceRepository;
            _carRepository = carRepository;
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<CarServiceResponse> Handle(AddCarServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };
            
            var client = await _clientRepository.GetClientByName(request._carService.ClientName);
            if (client == null) return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Client" };

            var car = await _carRepository.GetCarByPlateNumber(request._carService.CarPlateNumber);
            if (car == null) return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Car" };

            var carService = new CarService()
            {
                ClientId = client.Id,
                CarId = car.Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                ManipulationDescription = request._carService.ManipulationDescription,
                MDifficult = request._carService.MDifficult 
            };

            await _carServiceRepository.AddCarService(carService);
            _logger.LogInformation($"The Car Service : {carService.Id} / {DateTime.UtcNow} {carService.ManipulationDescription} was added to SQL DB");
            return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, CarService = carService };
        }
    }
}
