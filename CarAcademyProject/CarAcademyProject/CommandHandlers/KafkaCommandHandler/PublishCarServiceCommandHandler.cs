using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels.MediatR.KafkaCommands;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.KafkaCommandHandler
{
    public class PublishCarServiceCommandHandler : IRequestHandler<PublishCarServiceCommand, CarServiceResponse>
    {
        private readonly IKafkaPublisherService<int, PublishCarServiceRequest> _kafkaPublisher;
        private readonly ICarRepository _carRepository;
        private readonly IClientRepository _clientRepository;

        public PublishCarServiceCommandHandler(IKafkaPublisherService<int, PublishCarServiceRequest> kafkaPublisher, IClientRepository clientRepository, ICarRepository carRepository)
        {
            _kafkaPublisher = kafkaPublisher;
            _clientRepository = clientRepository;
            _carRepository = carRepository;
        }

        public async Task<CarServiceResponse> Handle(PublishCarServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var client = await _clientRepository.GetClientByName(request._carServiceRequest.ClientName);
            if (client == null) return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Client" };

            var car = await _carRepository.GetCarByPlateNumber(request._carServiceRequest.CarPlateNumber);
            if (car == null) return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Car" };

            var publishModel = new PublishCarServiceRequest()
            {
                ClientName = request._carServiceRequest.ClientName,
                CarPlateNumber = request._carServiceRequest.CarPlateNumber,
                ManipulationDescription = request._carServiceRequest.ManipulationDescription,
                MDifficult = request._carServiceRequest.MDifficult
            };

            await _kafkaPublisher.PublishTopic(client.Id, publishModel);
            return new CarServiceResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Message = "CarService was made, and is in progress"};
        }
    }
}
