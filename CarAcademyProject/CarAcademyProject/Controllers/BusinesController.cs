using CarAcademyProjectBL.BusinesService;
using Microsoft.AspNetCore.Mvc;

namespace CarAcademyProject.Controllers
{
    [ApiController]
    [Route("controller")]
    public class BusinesController : ControllerBase
    {
        private readonly ILogger<BusinesController> _logger;
        private readonly IRepostService _repostService;

        public BusinesController(ILogger<BusinesController> logger, IRepostService repostService)
        {
            _logger = logger;
            _repostService = repostService;
        }

        [HttpGet(nameof(GetClientCarServices))]
        public async Task<IActionResult> GetClientCarServices(string name)
        {
            var result = await _repostService.GetClientServices(name);
            return Ok(result);
        }

        [HttpGet(nameof(GetCarServicesByDates))]
        public async Task<IActionResult> GetCarServicesByDates(string startDate, string endDate)
        {
            var result = await _repostService.GetCarServicesForTime(startDate, endDate);
            return Ok(result);
        }
    }
}
