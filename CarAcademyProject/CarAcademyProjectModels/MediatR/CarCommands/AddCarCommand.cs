using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.CarCommands
{
    public record AddCarCommand(AddCarRequest car) : IRequest<AddCarResponse>
    {
        public readonly AddCarRequest _car = car;
    }
}
