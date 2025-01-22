using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SprintEvaluationAPI.Services
{
public class VideoService : IVideoService
{
    private readonly HttpClient _httpClient;

    public VideoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ProcessVideo(string videoUrl)
    {
        var payload = new { VideoUrl = videoUrl };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // Correct URL to avoid issues
        var response = await _httpClient.PostAsync("http://127.0.0.1:8000/process-video", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Failed to process video. Status: {response.StatusCode}");
    }
}


}
