using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SprintEvaluationAPI.Data;
using SprintEvaluationAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Registers controller services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IVideoService, VideoService>(); // Registers the VideoService for HTTP calls

// Configure database context with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionString"),
    sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Adds retry logic for transient failures
    }));

// Add CORS policy for frontend/backend communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Global error handling using the default ASP.NET Core middleware
app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sprint Evaluation API v1");
    c.RoutePrefix = string.Empty; // Makes Swagger available at the root URL
});

// Enforce HTTPS and HSTS for production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

// Enable CORS
app.UseCors("AllowAllOrigins");

app.UseRouting();

app.UseAuthentication(); // Enable authentication middleware if needed
app.UseAuthorization(); // Enable authorization middleware if needed

app.MapControllers(); // Maps controller endpoints

app.Run();
