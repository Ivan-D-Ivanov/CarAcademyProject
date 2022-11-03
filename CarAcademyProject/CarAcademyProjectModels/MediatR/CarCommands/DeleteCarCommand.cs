using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.CarCommands
{
    public record DeleteCarCommand(string plateNumber) : IRequest<AddCarResponse>
    {
        public readonly string _platNumber = plateNumber;
    }
}
