using Api.Models.Policy;

namespace UnitTesting_Api
{
    public class CoverPolicyTests
    {
        [Fact]
        [Trait("TestCategory", "L0")]
        public void CalculateRate_WhenDurationNotSet_ThenExceptionThrown()
        {
            // arrange
            ICoverPolicy sut = new CoverPolicy();

            // act
            Action rate = () => sut.CalculateRate();

            // assert
            Assert.Throws<NullReferenceException>(rate);
        }

        [Fact]
        [Trait("TestCategory", "L0")]
        public void CalculateRate_WhenDurationSet_ThenNoExceptionThrown()
        {
            // arrange
            ICoverPolicy sut = new CoverPolicy();

            // act 
            var price = sut.SetDuration(0).SetCover(0).SetPeriod(0).CalculateRate();

            // assert
            Assert.IsType<decimal>(price);
        }

        [Fact]
        [Trait("TestCategory", "L0")]
        public void CalculateRate_WhenCoverNotSet_ThenNoExceptionThrown()
        {
            // arrange
            ICoverPolicy sut = new CoverPolicy();

            // act
            Action rate = () => sut.SetDuration(0).CalculateRate();

            // assert
            Assert.Throws<NullReferenceException>(rate);
        }

        [Fact]
        [Trait("TestCategory", "L0")]
        public void CalculateRate_WhenPeriodNotSet_ThenNoExceptionThrown()
        {
            // arrange
            ICoverPolicy sut = new CoverPolicy();

            // act
            Action rate = () => sut.SetDuration(0).SetCover(0).CalculateRate();

            // assert
            Assert.Throws<NullReferenceException>(rate);
        }
    }
}