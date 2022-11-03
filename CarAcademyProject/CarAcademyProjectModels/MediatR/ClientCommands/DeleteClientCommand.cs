using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.ClientCommands
{
    public record DeleteClientCommand(string clientName) : IRequest<ClientResponse>
    {
        public readonly string _clientName = clientName;
    }
}
