using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.ClientCommands;
using MediatR;

namespace CarAcademyProject.CommandHandlers.ClientHandlers
{
    public class GetAllClientCommandHandler : IRequestHandler<GetAllClientsCommand, IEnumerable<Client>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> Handle(GetAllClientsCommand request, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetAllClients();
        }
    }
}
