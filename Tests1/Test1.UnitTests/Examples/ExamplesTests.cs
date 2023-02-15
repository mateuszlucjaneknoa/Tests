using FluentAssertions;
using Moq;
using System.Reflection;
using Test1.Domain.Pricing;
using Xunit;

namespace Test1.UnitTests.Examples
{
    public class ExamplesTests
    {
        [Fact]
        public void ExampleSClass_HasDefaultConstructor()
        {
            //Arrange
            var testType = Assembly.GetExecutingAssembly().GetType("Test1.UnitTests.Examples.ExamplesTests");

            //Act
            var instance = Activator.CreateInstance(testType!);

            //Assert
            Assert.NotNull(instance);
        }

        [InlineData(1, 2, false)]
        [InlineData(2, 1, true)]
        [Theory]
        public void TestCases(int first, int second, bool expected)
        {
            //Act
            var actual = first > second;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FluentAssertions()
        {
            (int number, DateTime date) first = (5, DateTime.UtcNow);
            (int number, DateTime date) second = (5, DateTime.UtcNow.AddDays(1));

            first.Should().BeEquivalentTo(second, options => options.Excluding(x => x.date));
        }

        [Fact]
        public void Stub()
        {
            /*
            //Arragng
            var stub = new MySpreadCalculatorStub();

            var dummyRisk = new Risk(1, 1, 1, 1);
            var expected = new Spread(1, 1, 1, 1);


            var systemUnderTest = new Examples.DummyClass(stub);

            //Act
            var actual = systemUnderTest.Calculate(dummyRisk);

            //Assert
            Assert.Equal(expected.NominalPrice, actual.NominalPrice);
            */
        }

        [Fact]
        public void Mock()
        {
            //Arragng
            var mock = new Mock<ISpreadCalculator>();

            var dummyRisk = new Risk(1, 1, 1, 1, RiskBracket.Medium);
            var expected = new Spread(1, 1, 1, 1);

            mock.Setup(x => x.GetSpread(dummyRisk, 1)).Returns(expected);

            var instance = mock.Object;

            //Act
            var actual = instance.GetSpread(dummyRisk, 1);

            //Assert
            Assert.Equal(expected, actual);

            mock.Verify(x => x.GetSpread(dummyRisk, 1), Times.Once);
        }

        [Fact]
        public void Mock2()
        {
            //Arrange
            var mock = new Mock<ISpreadCalculator>();

            var dummyRisk = new Risk(1, 1, 1, 1, RiskBracket.Medium);
            var expected = new Spread(1, 1, 1, 1);

            mock.Setup(x => x.GetSpread(It.IsAny<Risk>(), It.IsAny<decimal>())).Returns(expected);

            var systemUnderTest = new Examples.DummyClass(mock.Object);

            //Act
            var actual = systemUnderTest.Calculate(dummyRisk);

            //Assert
            Assert.Equal(expected, actual);

            mock.Verify(x => x.GetSpread(dummyRisk, 1), Times.Once);
        }
    }
}