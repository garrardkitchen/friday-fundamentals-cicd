using Newtonsoft.Json;
using NuGet.Protocol;
using Web.Models;

namespace Web.Services
{
    public class CoverPolicyService : ICoverPolicyService
    {
        private readonly ILogger<CoverPolicyService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CoverPolicyService(ILogger<CoverPolicyService> logger, IHttpClientFactory httpClientFactory)
        {
            this._logger = logger;
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<decimal> CalculatePolicy(CoverPolicyViewModel vm)
        {
            var httpClient = _httpClientFactory.CreateClient("Api");
            var httpResponseMessage = await httpClient.GetAsync($"CoverPolicy?numberOfPeople={vm.People}&cover={vm.CoverType}&period={vm.PeriodType}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream =
                    await httpResponseMessage.Content.ReadAsStringAsync();

                PolicyCoverResponse? response = JsonConvert.DeserializeObject<PolicyCoverResponse>(contentStream);
                _logger.LogInformation(response.ToJson());
                return response?.Cost ?? 0.0m;
            }

            return 0;
        }        
    }
}