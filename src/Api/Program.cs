using Api.Models.Configuration;
using Api.Models.Policy;
using Api.Models.Requests;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings and make available via DI
var configuration = builder.Configuration.GetSection(nameof(ApiOptions));

// make options available here, too
ApiOptions _apiOptions = new ApiOptions();
configuration.Bind(_apiOptions);

// Add services to the container.
builder.Services.Configure<ApiOptions>(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICoverPolicy, CoverPolicy>();
builder.Services.AddScoped<ProcessRequest>();
builder.Services.AddRateLimiter(_ =>
    _.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = _apiOptions.ThrottlePermitLimit;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = _apiOptions.ThrottleQueueLimit;
    })
);
builder.Services.AddTransient<Tracker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<Tracker>();
app.UseAuthorization();
app.UseRateLimiter();

// apply throttling across all endpoints
app.MapControllers().RequireRateLimiting("fixed");
app.Run();
