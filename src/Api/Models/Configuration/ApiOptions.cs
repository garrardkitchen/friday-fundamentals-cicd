namespace Api.Models.Configuration
{
    public class ApiOptions
    {
        public int ThrottlePermitLimit { get; set; }
        public int ThrottleQueueLimit { get; set; }
    }
}
