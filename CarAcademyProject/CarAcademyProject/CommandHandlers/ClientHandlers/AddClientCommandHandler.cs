using AutoMapper;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.ClientHandlers
{
    public class AddClientCommandHandler : IRequestHandler<AddClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public AddClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var resultFromMap = _mapper.Map<Client>(request._client);
            await _clientRepository.AddClient(resultFromMap);

            return new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Client = resultFromMap };
        }
    }
}
