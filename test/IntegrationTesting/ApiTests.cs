using Refit;

namespace IntegrationTesting
{
    public class ApiTests
    {
        [Fact]      
        [Trait("TestCategory", "L3")]
        public async Task GetEstimate_WhenAtleastOneQSParameterNotPassed_ReturnNotFound()
        {
            // arrange
            var api = RestService.For<IApi>("http://localhost:5289");

            // act
            var response = await api.GetEstimate(1);
            
            // assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response?.StatusCode);
        }

        [Theory]
        [InlineData(1, 0, 0, 6.4)]
        [InlineData(1, 1, 0, 3.1)]
        [InlineData(1, 2, 0, 1.5)]
        [Trait("TestCategory", "L3")]
        public async Task GetEstimate_WhenDurationAndCoverAndPeriodPassedAsQS_ReturnEstimate(int duration, int cover, int period, decimal estimate)
        {
            // arrange
            var api = RestService.For<IApi>("http://localhost:5289");

            // act
            var response = await api.GetEstimate(duration, cover, period);

            // assert
            Assert.IsType<PolicyCoverResponse>(response);
            Assert.Equal(estimate, response.Cost);
        }
    }

    public interface IApi
    {
        [Get("/CoverPolicy?numberOfPeople={duration}&cover={cover}&period={period}")]
        Task<PolicyCoverResponse> GetEstimate(int duration, int cover, int period);


        [Get("/CoverPolicy?numberOfPeople={duration}")]
        Task<ApiResponse<PolicyCoverResponse>> GetEstimate(int duration);
    }

    public class PolicyCoverResponse
    {
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public decimal Cost { get; set; }

        public decimal Rate { get; set; }
    }
}