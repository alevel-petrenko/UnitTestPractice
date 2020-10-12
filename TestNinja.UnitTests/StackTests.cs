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
			Assert.That(stack.Peek(), Is.EqualTo(value));
		}

		[Test]
		public void Push_StringHasValue_StackCountIncrements()
		{
			//
			// Arrange.
			//
			int expectedCount = 1;

			//
			// Act.
			//
			stack.Push("a");
			var actualCount = stack.Count;

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
			string expected = "a";

			//
			// Act.
			//
			stack.Push(expected);
			var actual = stack.Pop();

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
			int expected = 1;

			//
			// Act.
			//
			stack.Push("a");
			stack.Push("a");
			stack.Pop();
			var actual = stack.Count;

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
			int expected = 2;

			//
			// Act.
			//
			stack.Push("a");
			stack.Push("a");
			stack.Peek();
			var actual = stack.Count;

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
			string expected = "a";

			//
			// Act.
			//
			stack.Push(expected);
			var actual = stack.Peek();

			//
			// Assert.
			//
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}