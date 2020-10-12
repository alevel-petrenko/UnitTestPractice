using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public sealed class StackTests
	{
		private Stack<string> stack;

		[SetUp]
		public void Setup()
		{
			stack = new Stack<string>();
		}

		[Test]
		public void Push_StringIsNull_GetArgumentNullException()
		{
			//
			// Arrange.
			//
			string value = null;

			//
			// Assert.
			//
			Assert.That(() => stack.Push(value), Throws.ArgumentNullException);
		}

		[Test]
		public void Push_StringHasValue_ValueIsAddedToStack()
		{
			//
			// Arrange.
			//
			string value = "a";

			//
			// Act.
			//
			stack.Push(value);

			//
			// Assert.
			//
			Assert.That(value, Is.EqualTo(stack.Peek()));
		}

		[Test]
		public void Push_StringHasValue_StackCountIncrements()
		{
			//
			// Arrange.
			//
			int actualCount = 1;

			//
			// Act.
			//
			stack.Push("a");
			var expectedCount = stack.Count;

			//
			// Assert.
			//
			Assert.That(actualCount, Is.EqualTo(expectedCount));
		}

		[Test]
		public void Pop_CallWhenNoItems_GetInvalidOperationException()
		{
			//
			// Assert.
			//
			Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
		}

		[Test]
		public void Pop_CallWhenItemsExist_ReturnsNewlyAddedItem()
		{
			//
			// Arrange.
			//
			string actual = "a";

			//
			// Act.
			//
			stack.Push(actual);
			var expected = stack.Pop();

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Pop_CallWhenItemsExist_StackCountDecrements()
		{
			//
			// Arrange.
			//
			int actual = 1;

			//
			// Act.
			//
			stack.Push("a");
			stack.Push("a");
			stack.Pop();
			var expected = stack.Count;

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Peek_CallWhenItemsExist_StackCountNotDecrements()
		{
			//
			// Arrange.
			//
			int actual = 2;

			//
			// Act.
			//
			stack.Push("a");
			stack.Push("a");
			stack.Peek();
			var expected = stack.Count;

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Peek_CallWhenItemsNoExist_GetInvalidOperationException()
		{
			//
			// Assert.
			//
			Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
		}

		[Test]
		public void Peek_CallWhenItemsExist_ReturnsNewlyAddedItem()
		{
			//
			// Arrange.
			//
			string actual = "a";

			//
			// Act.
			//
			stack.Push(actual);
			var expected = stack.Peek();

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}