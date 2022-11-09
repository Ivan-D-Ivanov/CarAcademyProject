using System.Net;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _mediator.Send(new GetAllClientsCommand()));
        }

        [HttpPost(nameof(AddClient))]
        public async Task<IActionResult> AddClient(ClientRequest client)
        {
            var result = await _mediator.Send(new AddClientCommand(client));
            if (result.HttpStatusCode == HttpStatusCode.BadRequest) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateClientByName))]
        public async Task<IActionResult> UpdateClientByName(ClientRequest client)
        {
            var result = await _mediator.Send(new UpdateClientCommand(client));
            if (result.HttpStatusCode == HttpStatusCode.BadRequest) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteClient))]
        public async Task<IActionResult> DeleteClient(string name)
        {
            var result = await _mediator.Send(new DeleteClientCommand(name));
            if (result.HttpStatusCode == HttpStatusCode.BadRequest) return BadRequest(result);
            return Ok(result);
        }
    }
}
