using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
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
        private readonly IKafkaPublisherService<int, CarService> _kafkaPublisherService;


        public CarServiceController(ILogger<CarServiceController> logger, IMediator mediator, IKafkaPublisherService<int, CarService> kafkaPublisherService)
        {
            _logger = logger;
            _mediator = mediator;
            _kafkaPublisherService = kafkaPublisherService;
        }

        [HttpGet(nameof(GetCarServices))]
        public async Task<IEnumerable<CarService>> GetCarServices()
        {
            return await _mediator.Send(new GetAllCarServiceCommand());
        }

        [HttpPost(nameof(AddCarService))]
        public async Task<IActionResult> AddCarService(CarServiceRquest carService)
        {
            //Add KafkaPublisher insted of directly saving to database
            var result = await _mediator.Send(new AddCarServiceCommand(carService));
            return Ok(result);
        }
    }
}
