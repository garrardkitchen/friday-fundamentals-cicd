namespace Web.Models.Configuration
{
    public class AppOptions
    {
        public string? ApiUri { get; set; }
        public int ThrottlePermitLimit { get; set; }
        public int ThrottleQueueLimit { get; set; }
    }
}
