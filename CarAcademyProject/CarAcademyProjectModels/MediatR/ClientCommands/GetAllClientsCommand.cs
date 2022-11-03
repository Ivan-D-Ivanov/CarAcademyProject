using MediatR;

namespace CarAcademyProjectModels.MediatR.ClientCommands
{
    public class GetAllClientsCommand : IRequest<IEnumerable<Client>>
    {
    }
}
