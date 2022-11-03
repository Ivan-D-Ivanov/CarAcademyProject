using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.ClientCommands
{
    public record UpdateClientCommand(ClientRequest client) : IRequest<ClientResponse>
    {
        public readonly ClientRequest _client = client;
    }
}
