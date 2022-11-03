using MediatR;

namespace CarAcademyProjectModels.MediatR.CarServiceCommands
{
    public class GetAllCarServiceCommand : IRequest<IEnumerable<CarService>>
    {
    }
}
