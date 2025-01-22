using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TeamProject.Function
{
    public class VideoProcessorTrigger
    {
        private readonly ILogger<VideoProcessorTrigger> _logger;

        public VideoProcessorTrigger(ILogger<VideoProcessorTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(VideoProcessorTrigger))]
        public async Task Run(
            [BlobTrigger("videos/{name}", Connection = "AzureWebJobsStorage")] Stream stream, 
            string name)
        {
            _logger.LogInformation($"Blob trigger function started processing blob: {name}");

            try
            {
                // Reading blob content
                using var blobStreamReader = new StreamReader(stream);
                var content = await blobStreamReader.ReadToEndAsync();

                // Log size information for debugging
                _logger.LogInformation($"Blob name: {name} | Blob size: {stream.Length} bytes");

                // Add logic to process the content (e.g., send to Python API for pose analysis)
                await ProcessVideo(content, name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while processing blob {name}: {ex.Message}");
                throw; // Rethrow to allow Azure to retry or handle as needed
            }
        }

        private async Task ProcessVideo(string content, string name)
        {
            // Placeholder for video processing logic
            // This could involve calling the Python API or storing data in SQL Database
            _logger.LogInformation($"Processing video: {name}");

            // Simulate processing delay
            await Task.Delay(500);

            _logger.LogInformation($"Video {name} processing completed.");
        }
    }
}
