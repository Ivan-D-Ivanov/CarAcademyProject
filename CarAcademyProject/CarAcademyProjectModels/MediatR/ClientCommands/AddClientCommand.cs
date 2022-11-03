using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProjectModels.MediatR.ClientCommands
{
    public record AddClientCommand(ClientRequest client) : IRequest<ClientResponse>
    {
        public readonly ClientRequest _client = client;
    }
}
