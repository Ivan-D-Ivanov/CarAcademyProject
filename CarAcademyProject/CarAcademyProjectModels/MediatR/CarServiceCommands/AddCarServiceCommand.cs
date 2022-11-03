using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.CarServiceCommands
{
    public record AddCarServiceCommand(CarServiceRquest carService) : IRequest<CarServiceResponse>
    {
        public readonly CarServiceRquest _carService = carService;
    }
}
