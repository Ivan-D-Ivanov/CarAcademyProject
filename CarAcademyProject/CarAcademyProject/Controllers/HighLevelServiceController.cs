using CarAcademyProjectBL.HighLevelServices;
using Microsoft.AspNetCore.Mvc;

namespace CarAcademyProject.Controllers
{
    [ApiController]
    [Route("controller")]
    public class HighLevelServiceController : ControllerBase
    {
        private readonly HighLevelRepoService _highLevelRepo;

        public HighLevelServiceController(HighLevelRepoService highLevelRepo)
        {
            _highLevelRepo = highLevelRepo;
        }

        [HttpGet(nameof(GetHighLevelServices))]
        public async Task<IActionResult> GetHighLevelServices(string name, string plateNumber)
        {
            var result = await _highLevelRepo.GetCarServices(name, plateNumber);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteHighLevelService))]
        public async Task<IActionResult> DeleteHighLevelService(string name, string plateNumber)
        {
            var result = await _highLevelRepo.DeleteCarService(name, plateNumber);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPut(nameof(FinishHighLevelService))]
        public async Task<IActionResult> FinishHighLevelService(string name, string plateNumber)
        {
            var result = await _highLevelRepo.FinishHighLevelCarService(name, plateNumber);
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
