using Imagination.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
namespace Imagination.Services
{
    public class ImaginationService: IImaginationService
    {
        private readonly ILogger<ImaginationService> _logger;
        public ImaginationService(ILogger<ImaginationService> logger)
        {
            _logger= logger;
        }

        public async Task<CoversionResponse> Convert(Stream sourceStream)
        {
            try
            {
                using (MemoryStream sourceStgStream = new MemoryStream())
                {
                    await sourceStream.CopyToAsync(sourceStgStream);
                    using (MemoryStream convertedStream = new MemoryStream())
                    {
                        sourceStgStream.Position = 0;
                        var image = await Image.LoadWithFormatAsync(sourceStgStream, CancellationToken.None);
                        await image.Image.SaveAsJpegAsync(convertedStream, CancellationToken.None);

                        _logger.LogInformation("PNG to JPEG file conversion successfull");
                        return new CoversionResponse
                        {
                            Status = true,
                            Message = "Conversion successfull",
                            TargatedStream = convertedStream.ToArray()
                        };                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Conversion Failed", ex.Message);
                return new CoversionResponse
                {
                    Status = false,
                    Message = "Conversion Failed"
                };
            }
        }
    }
}
