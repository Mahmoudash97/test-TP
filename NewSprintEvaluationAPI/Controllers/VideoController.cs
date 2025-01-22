using Microsoft.AspNetCore.Mvc;
using SprintEvaluationAPI.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;

    public VideoController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessVideo([FromBody] VideoRequest request)
    {
        if (string.IsNullOrEmpty(request.VideoUrl))
        {
            return BadRequest(new { error = "Video URL is required." });
        }

        try
        {
            var result = await _videoService.ProcessVideo(request.VideoUrl);

            // Parse the Python response to JSON
            return Content(result, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while processing the video.", details = ex.Message });
        }
    }
}

// Request model for JSON deserialization
public class VideoRequest
{
    public string VideoUrl { get; set; }
}
