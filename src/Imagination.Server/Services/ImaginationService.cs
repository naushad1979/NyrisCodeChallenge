using Imagination.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace Imagination.Services
{
    public class ImaginationService: IImaginationService
    {
        private readonly ILogger<ImaginationService> _logger;
        public ImaginationService(ILogger<ImaginationService> logger)
        {
            _logger= logger;
        }

        public async Task<CoversionResponse> ConvertAsync(Stream sourceStream)
        {
            using var activity = Program.Telemetry.StartActivity("Coversion Started");
            using var scope = _logger.BeginScope("Incoming Image processing started");
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

                        _logger.LogInformation("Image conversion successfull");
                        activity?.SetStatus(Status.Ok);
                        activity.Stop();

                        return new CoversionResponse
                        {
                            Status = true,
                            Message = "Conversion successfull",
                            TargatedStream = convertedStream.ToArray()
                        };                        
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Conversion Failed", e.Message);
                activity?.SetStatus(Status.Error);
                activity?.AddEvent(new ActivityEvent(e.Message));
                activity.Stop();

                return new CoversionResponse
                {
                    Status = false,
                    Message = "Conversion Failed"
                };
            }
        }
    }
}
