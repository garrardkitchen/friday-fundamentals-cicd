namespace Api.Models.Policy
{
    public class PolicyCoverResponse
    {
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public decimal Cost { get; set; }

        public decimal Rate { get; set; }
    }
}