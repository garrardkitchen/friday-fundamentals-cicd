using Web.Models;

namespace Web.Services
{
    public interface ICoverPolicyService
    {
        //Task<decimal> CalculatePolicy(int numberOfPeople);
        Task<decimal> CalculatePolicy(CoverPolicyViewModel vm);
    }
}