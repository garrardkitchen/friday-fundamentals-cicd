namespace Api.Models.Requests
{
    public struct RequestParameters
    {
        public RequestParameters()
        {            
        }

        public RequestParameters(int duration, int cover, int period)
        {
            Duration = duration;
            Cover = cover;
            Period = period;
        }

        public int Duration { get; set; }
        public int Cover { get; set; }
        public int Period { get; set; }
    }
}
