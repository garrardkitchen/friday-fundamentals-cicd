namespace Api.Models.Policy
{
    public interface ICoverPolicy
    {
        decimal CalculateRate();
        CoverPolicy SetCover(int cover);
        CoverPolicy SetDuration(int duration);
    }
}