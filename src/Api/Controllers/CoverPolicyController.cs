using Api.Models;
using Api.Models.Policy;
using Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoverPolicyController : ControllerBase
    {
        private readonly ILogger<CoverPolicyController> _logger;
        private readonly ICoverPolicy _coverPolicy;
        private readonly ProcessRequest _processRequest;

        public CoverPolicyController(ILogger<CoverPolicyController> logger, ICoverPolicy coverPolicy, ProcessRequest processRequest)
        {
            _logger = logger;
            this._coverPolicy = coverPolicy;
            this._processRequest = processRequest;
        }

        [HttpGet(Name = "GetPolicyCost/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json", Type = typeof(PolicyCoverResponse))]
        public IActionResult Get()
        {
            string? duration = this.HttpContext.Request.Query.FirstOrDefault(x => x.Key == "numberOfPeople").Value;
            string? cover = this.HttpContext.Request.Query.FirstOrDefault(x => x.Key == "cover").Value;
            string? period = this.HttpContext.Request.Query.FirstOrDefault(x => x.Key == "period").Value;

            _logger.LogInformation("cover={cover}, period={period}", @cover, @period);

            var result = _processRequest.TryProcess(duration, cover, period);

            if (result.IsFailure)
            {                
                return NotFound(new PolicyCoverResponse() { Cost = 0.0m });                
            }

            PolicyCoverResponse response = new()
            {
                Date = DateTime.Now,
                Duration = result.Value.Duration,
                Cost = _coverPolicy
                        .SetDuration(result.Value.Duration)
                        .SetCover(result.Value.Cover)
                        .SetPeriod(result.Value.Period)
                        .CalculateRate(),
                Rate = Rates.HOUSE_RATE
            };

            return Ok(response);
        }
    }
}