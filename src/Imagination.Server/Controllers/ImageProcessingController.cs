using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Imagination.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageProcessingController : ControllerBase
    {        
        private readonly ILogger<ImageProcessingController> _logger;

        public ImageProcessingController(ILogger<ImageProcessingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Get")]
        public string Get()
        {
            return "ImageProcessing Service running...";
        }         
    }
}
