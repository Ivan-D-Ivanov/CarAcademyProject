using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly IMediator _mediator;


        public CarController(ILogger<CarController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(nameof(GetCars))]
        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _mediator.Send(new GetAllCarsCommand());
        }

        [HttpPost(nameof(AddCar))]
        public async Task<IActionResult> AddCar(AddCarRequest car)
        {
            var result = await _mediator.Send(new AddCarCommand(car));
            return Ok(result);
        }

        [HttpPut(nameof(UpdateCar))]
        public async Task<IActionResult> UpdateCar(AddCarRequest car)
        {
            var result = await _mediator.Send(new UpdateCarCommand(car));
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteCar))]
        public async Task<IActionResult> DeleteCar(string plateNumber)
        {
            var result = await _mediator.Send(new DeleteCarCommand(plateNumber));
            return Ok(result);
        }
    }
}