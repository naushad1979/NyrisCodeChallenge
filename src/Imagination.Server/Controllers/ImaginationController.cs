using Imagination.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Imagination.Controllers
{
    [ApiController]
    public class ImaginationController : ControllerBase
    {
        private readonly ILogger<ImaginationController> _logger;
        private readonly IImaginationService _imaginationService;

        public ImaginationController(ILogger<ImaginationController> logger
            , IImaginationService imaginationService)
        {
            _logger = logger;
            _imaginationService = imaginationService;
        }


        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> ConvertAsync()
        {
            using var activity = Program.Telemetry.StartActivity("Service png to jpg conversion start");
            using var scope = _logger.BeginScope("Processing PNG stream started");

            var response = await _imaginationService.Convert(Request.Body);

            if (response != null && response.Status)
            {
                return File(response.TargatedStream, "image/jpeg");
            }
            else
            {
                return BadRequest(response);
            }                                   
        }
    }
}
