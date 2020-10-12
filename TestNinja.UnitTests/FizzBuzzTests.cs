using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public sealed class FizzBuzzTests
	{
		[Test]
		[TestCase(0, "FizzBuzz")]
		[TestCase(3, "Fizz")]
		[TestCase(5, "Buzz")]
		[TestCase(2, "2")]
		public void GetOutput_WhenCalled_ReturnsCorrectValue(int input, string output)
		{
			//
			// Act.
			//
			var result = FizzBuzz.GetOutput(input);

			//
			// Assert.
			//
			Assert.That(result, Is.EqualTo(output));
		}
	}
}