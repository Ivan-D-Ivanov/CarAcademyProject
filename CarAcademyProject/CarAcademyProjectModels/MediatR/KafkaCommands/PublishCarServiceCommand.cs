using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.KafkaCommands
{
    public record PublishCarServiceCommand(PublishCarServiceRequest carServiceRequest) : IRequest<CarServiceResponse> 
    {
        public readonly PublishCarServiceRequest _carServiceRequest = carServiceRequest;
    }
}
