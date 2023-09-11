using Api.Controllers;
using Api.Models.Policy;
using Api.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTesting_Api
{
    public class CoverPolicyControllerTests
    {
        IServiceProvider _serviceProvider;

        public CoverPolicyControllerTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddScoped<ICoverPolicy, CoverPolicy>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        [Trait("TestCategory", "L1")]
        public void Get_MissingCoverAndPeriod_ReturnsNotFoundObjectResult()
        {
            // arrange
            var scope = _serviceProvider.CreateScope();
            var coverPolicy = scope.ServiceProvider.GetService<ICoverPolicy>();
            var loggerMock = new Mock<ILogger<CoverPolicyController>>();
            var processRequestMock = new Mock<ProcessRequest>();
#pragma warning disable CS8604 // Possible null reference argument.
            CoverPolicyController sut = new CoverPolicyController(loggerMock.Object, coverPolicy, processRequestMock.Object);
#pragma warning restore CS8604 // Possible null reference argument.
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.ControllerContext.HttpContext.Request.QueryString = new QueryString("?numberOfPeople=1");

            // act
            var response = sut.Get();            
            
            // assert
            Assert.IsType<NotFoundObjectResult>(response);            
        }

        [Theory]
        [InlineData(1, 0, 0, 6.4)]
        [InlineData(1, 1, 0, 3.1)]
        [InlineData(1, 2, 0, 1.5)]
        [Trait("TestCategory", "L1")]
        public void GetEstimate_WhenDurationAndCoverAndPeriodPassedAsQS_ReturnEstimate(int duration, int cover, int period, decimal estimate)
        {
            // arrange
            var scope = _serviceProvider.CreateScope();
            var coverPolicy = scope.ServiceProvider.GetService<ICoverPolicy>();
            var loggerMock = new Mock<ILogger<CoverPolicyController>>();
            var processRequestMock = new Mock<ProcessRequest>();
#pragma warning disable CS8604 // Possible null reference argument.
            CoverPolicyController sut = new CoverPolicyController(loggerMock.Object, coverPolicy, processRequestMock.Object);
#pragma warning restore CS8604 // Possible null reference argument.
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.ControllerContext.HttpContext.Request.QueryString = new QueryString($"?numberOfPeople={duration}&cover={cover}&period={period}");

            // act
            var response = sut.Get();
            var result = response as OkObjectResult;
            var resultModel = result?.Value as PolicyCoverResponse;

            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.IsType<PolicyCoverResponse>(resultModel);
            Assert.Equal(estimate, resultModel?.Cost);
        }
    }
}