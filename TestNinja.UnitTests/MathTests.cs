using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public sealed class MathTests
	{
		private Math math;

		[SetUp]
		public void TestSetup()
		{
			math = new Math();
		}

		[Test]
		[Ignore("For the time being")]
		public void Math_Add_PassOneAndFive_ReturnsSix()
		{
			//
			// Act.
			//
			int expected = 6;

			//
			// Act.
			//
			int actual = math.Add(1, 5);

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		[TestCase(1, 0, 1)]
		[TestCase(0, 1, 1)]
		[TestCase(1, 1, 1)]
		public void Math_Max_WhenCalled_ReturnMaxArgument(int first, int second, int expected)
		{
			//
			// Act.
			//
			int actual = math.Max(first, second);

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}