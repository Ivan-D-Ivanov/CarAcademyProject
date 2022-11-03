using CarAcademyProjectModels.MediatR.CarCommands;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProject.CommandHandlers.ClientHandlers;

namespace CarAcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;


        public ClientController(ILogger<ClientController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(nameof(GetAllClients))]
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _mediator.Send(new GetAllClientsCommand());
        }

        [HttpPost(nameof(AddClient))]
        public async Task<IActionResult> AddClient(ClientRequest client)
        {
            var result = await _mediator.Send(new AddClientCommand(client));
            return Ok(result);
        }

        [HttpPut(nameof(UpdateClientByName))]
        public async Task<IActionResult> UpdateClientByName(ClientRequest client)
        {
            var result = await _mediator.Send(new UpdateClientCommand(client));
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteClient))]
        public async Task<IActionResult> DeleteClient(string name)
        {
            var result = await _mediator.Send(new DeleteClientCommand(name));
            return Ok(result);
        }
    }
}
