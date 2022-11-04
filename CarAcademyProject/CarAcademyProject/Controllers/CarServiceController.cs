using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.MediatR.KafkaCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarServiceController : ControllerBase
    {
        private readonly ILogger<CarServiceController> _logger;
        private readonly IMediator _mediator;


        public CarServiceController(ILogger<CarServiceController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(nameof(GetCarServices))]
        public async Task<IEnumerable<CarService>> GetCarServices()
        {
            return await _mediator.Send(new GetAllCarServiceCommand());
        }

        [HttpPost(nameof(AddCarService))]
        public async Task<IActionResult> AddCarService(PublishCarServiceRequest carService)
        {
            var result = await _mediator.Send(new PublishCarServiceCommand(carService));
            return Ok(result);
        }
    }
}
