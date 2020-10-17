using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
	[TestFixture]
	public sealed class DemeritPointsCalculatorTests
	{
		private DemeritPointsCalculator calculator;

		[SetUp]
		public void SetUp()
		{
			calculator = new DemeritPointsCalculator();
		}

		[Test]
		[TestCase(-1)]
		[TestCase(301)]
		public void CalculateDemeritPoints_PassNotRealSpeed_GetArgumentOutOfRangeException(int speed)
		{
			//
			// Assert.
			//
			Assert.That(() => calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
		}

		[Test]
		[TestCase(0, 0)]
		[TestCase(65, 0)]
		[TestCase(69, 0)]
		[TestCase(100, 7)]
		public void CalculateDemeritPoints_PassRealSpeedLimit_ReturnsCorrectAmountOfPoint(int speed, int expectedDemeritPoints)
		{
			//
			// Act.
			//
			var result = calculator.CalculateDemeritPoints(speed);

			//
			// Assert.
			//
			Assert.That(result, Is.EqualTo(expectedDemeritPoints));
		}
	}
}