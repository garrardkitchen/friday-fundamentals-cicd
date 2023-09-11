using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;
using Web.Models;
using Web.Services;

namespace UnitTesting_Web
{
    public class HomeControllerTests
    {
        [Fact]
        [Trait("TestCategory", "L1")]
        public void Index_ReturnCoverPolicyViewModel()
        {
            // arrange    
            var coverPolicyMock = new Mock<ICoverPolicyService>();
            var loggerMock = new Mock<ILogger<HomeController>>();
            HomeController sut = new HomeController(loggerMock.Object, coverPolicyMock.Object);
          
            // act
            var response = sut.Index();
            var result = response as ViewResult;
            
            // assert
            Assert.IsType<ViewResult>(response);
            Assert.IsType<CoverPolicyViewModel>(result?.Model);            
        }

        [Fact]
        [Trait("TestCategory", "L1")]
        public async Task CalculatePolicy_ReturnCoverPolicyViewModel_And_Estimate1_1m()
        {
            // arrange    
            decimal estimate = 1.1m;
            var coverPolicyMock = new Mock<ICoverPolicyService>();
            var loggerMock = new Mock<ILogger<HomeController>>();
            coverPolicyMock
                .Setup(x => x.CalculatePolicy(It.IsAny<CoverPolicyViewModel>()))
                .ReturnsAsync(estimate);
            HomeController sut = new HomeController(loggerMock.Object, coverPolicyMock.Object);

            // act
            var response = await sut.CalculatePolicy(new CoverPolicyViewModel());
            var result = response as ViewResult;

            // assert
            Assert.IsType<ViewResult>(response);
            Assert.IsType<CoverPolicyViewModel>(result?.Model);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal(estimate, (result?.Model as CoverPolicyViewModel).Cost);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}