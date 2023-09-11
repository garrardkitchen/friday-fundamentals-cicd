namespace Api.Models.Policy
{

    public sealed class CoverPolicy : ICoverPolicy
    {
        private int? _duration;
        private int? _cover;
        private int? _period;

        public CoverPolicy()
        {
        }

        public CoverPolicy SetDuration(int duration)
        {
            _duration = duration;
            return this;
        }

        public decimal CalculateRate()
        {
            ThrowIfMissingCriteria();
            decimal rate = 0.0m;

            switch (_cover)
            {
                case 0:
                    rate = Rates.HOUSE_RATE;
                    break;
                case 1:
                    rate = Rates.CAR_RATE;
                    break;
                case 2:
                    rate = Rates.HOLIDAY_RATE;
                    break;
                default:
                    throw new InvalidOperationException("Incorrect cover type");
            }

#pragma warning disable CS8629 // Nullable value type may be null.
            return (decimal)(rate * _duration);
#pragma warning restore CS8629 // Nullable value type may be null.
        }

        private void ThrowIfMissingCriteria()
        {
            if (_duration == null)
            {
                throw new NullReferenceException("Duration has not been set");
            }

            if (_cover == null)
            {
                throw new NullReferenceException("Cover has not been set");
            }

            if (_period == null)
            {
                throw new NullReferenceException("Period has not been set");
            }
        }

        public CoverPolicy SetCover(int cover)
        {
            _cover = cover;
            return this;
        }

        public CoverPolicy SetPeriod(int period)
        {
            _period = period;
            return this;
        }
    }
}