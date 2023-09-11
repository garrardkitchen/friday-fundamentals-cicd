using Api.Models.Requests;
using System;

namespace UnitTesting_Api
{
    public class ProcessRequestTests
    {
        [Fact]
        [Trait("TestCategory", "L0")]
        public void ProcessRequest_WhenInvalidParametersUsed_ReturnFailure()
        {
            // arrange
            var sut = new ProcessRequest();

            // act
            var result = sut.TryProcess("0", "", null);

            // assert
            Assert.True(result.IsFailure);
        }

        [Theory]
        [InlineData(null, "0",  "0",  true)]
        [InlineData("",   "0",  "0",  true)]
        [InlineData("A",  "0",  "0",  true)]
        [InlineData("0",  null, "0",  true)]
        [InlineData("0",  "",   "0",  true)]
        [InlineData("0",  "A",  "0",  true)]
        [InlineData("0",  "0",  null, true)]
        [InlineData("0",  "0",  "",   true)]
        [InlineData("0",  "0",  "A",  true)]
        [Trait("TestCategory", "L0")]
        public void ProcessRequest_WhenInvalidParameterUsed_ReturnFailure(string? duration, string? cover, string? period, bool failure)
        {
            // arrange
            var sut = new ProcessRequest();

            // act
            var result = sut.TryProcess(duration, cover, period);

            // assert
            Assert.Equal(failure, result.IsFailure);
        }

        [Fact]
        [Trait("TestCategory", "L0")]
        public void ProcessRequest_WhenValidParametersUsed_ReturnSuccess()
        {
            // arrange
            var sut = new ProcessRequest();

            // act
            var result = sut.TryProcess("0", "0", "0");

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(0, result.Value.Duration);
            Assert.Equal(0, result.Value.Cover);
            Assert.Equal(0, result.Value.Period);
        }
    }
}