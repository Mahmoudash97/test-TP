using Microsoft.EntityFrameworkCore;

namespace SprintEvaluationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Evaluation> Evaluations { get; set; }
    }

    public class Evaluation
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public float Score { get; set; }
        public string Feedback { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
