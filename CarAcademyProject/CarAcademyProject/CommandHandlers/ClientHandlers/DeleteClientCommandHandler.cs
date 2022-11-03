using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.ClientHandlers
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;

        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is empty" };

            var result = await _clientRepository.GetClientByName(request._clientName);
            if (result == null) return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This car does not exists" };

            await _clientRepository.DeleteClient(result.Id);
            return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Client = result };
        }
    }
}
