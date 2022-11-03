using AutoMapper;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.ClientHandlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (request._client == null) return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var name = request._client.FirstName + " " + request._client.LastName;
            var result = await _clientRepository.GetClientByName(name);
            if (result == null) return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Client" };

            var resultFromMap = _mapper.Map<Client>(request._client);
            resultFromMap.Id = result.Id;
            await _clientRepository.UpdateClient(resultFromMap);
            return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Client = resultFromMap };
        }
    }
}
