using Microsoft.Net.Http.Headers;
using Web.Services;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Web.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings and make available via DI
var configuration = builder.Configuration.GetSection(nameof(AppOptions));

// make options available here, too
AppOptions _appOptions = new AppOptions();
configuration.Bind(_appOptions);

builder.Services.AddDbContext<PolicyContext>(options =>
    options.UseInMemoryDatabase("test"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("PolicyContext") ?? throw new InvalidOperationException("Connection string 'PolicyContext' not found.")));
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("Api", httpClient =>
{
    var apiUri = _appOptions.ApiUri ?? throw new Exception("Missing ApiUri");
    httpClient.BaseAddress = new Uri(apiUri);

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "WebSample");
});
builder.Services.AddScoped<ICoverPolicyService, CoverPolicyService>();
builder.Services.AddRateLimiter(_ =>
    _.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = _appOptions.ThrottlePermitLimit;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = _appOptions.ThrottleQueueLimit;
    })
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireRateLimiting("fixed");

app.Run();
