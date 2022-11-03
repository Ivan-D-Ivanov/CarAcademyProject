using MediatR;

namespace CarAcademyProjectModels.MediatR.CarCommands
{
    public record GetAllCarsCommand : IRequest<IEnumerable<Car>>
    {
    }
}
