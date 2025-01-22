namespace SprintEvaluationAPI.Services
{
    public interface IVideoService
    {
        Task<string> ProcessVideo(string videoUrl);
    }
}
