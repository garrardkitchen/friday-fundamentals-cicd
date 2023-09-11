using CSharpFunctionalExtensions;

namespace Api.Models.Requests
{
    public class ProcessRequest
    {
        public Result<RequestParameters> TryProcess(string? duration, string? cover, string? period) 
        {
            if (!this.IsValidRequest(duration, cover, period))
            {
                return Result.Failure<RequestParameters>("At least one missing paramter");
            }

#pragma warning disable CS8604 // Possible null reference argument.
            return Result.Success(Convert(duration, cover, period));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private bool IsValidRequest(string? duration, string? cover, string? period)
        {
            if (null == duration ||
                !int.TryParse(duration, out int _) ||
                null == cover ||
                !int.TryParse(cover, out int _) ||
                null == period ||
                !int.TryParse(period, out int _))
            {
                return false;
            }

            return true;
        }

        private RequestParameters Convert(string duration, string cover, string period)
        {
            return new RequestParameters(int.Parse(duration), int.Parse(cover), int.Parse(period));
        }
    }
}
