using Imagination.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
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

        /// <summary>
        /// Input stream will accept as a Request body
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> ConvertAsync()
        {
            var response = await _imaginationService.ConvertAsync(Request.Body);

            if (response != null && response.Status)
            {
                return File(response.TargatedStream, "image/jpeg");
            }
            else
            {
                // returns 400 status code for with error details
                return BadRequest(response);
            }
        }
    }
}
