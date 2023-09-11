public class Tracker : IMiddleware
{
    private readonly ILogger<Tracker> _logger;

    public Tracker(ILogger<Tracker> logger)
    {
        this._logger = logger;
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Headers.ContainsKey("App-Tracker"))
        {
            _logger.LogInformation("Tracking {0}", 
                context.Request.Headers["App-Tracker"].ToString());
        }
        
        next(context);
        
        return Task.CompletedTask;
    }
}