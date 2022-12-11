using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace Imagination.Controllers
{
    [ApiController]
    public class ImaginationController : ControllerBase
    {        
        private readonly ILogger<ImaginationController> _logger;

        public ImaginationController(ILogger<ImaginationController> logger)
        {
            _logger = logger;
        }         


        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> ConvertAsync()
        {
            MemoryStream pngStream = new MemoryStream();
            await Request.Body.CopyToAsync(pngStream);
            
            pngStream.Position = 0;
            MemoryStream jpgStream = new MemoryStream();

            try
            {
                var image = await Image.LoadWithFormatAsync(pngStream, CancellationToken.None);
                await image.Image.SaveAsJpegAsync(jpgStream,CancellationToken.None);
            }
            catch (Exception ex)
            {
                throw;
            }

            return File(jpgStream.ToArray(), "image/jpeg");             
        }
    }
}
