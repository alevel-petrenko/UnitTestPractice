using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class EmployeeControllerTests
	{
		private Mock<IEmployeeRepository> employeeRepository;
		private EmployeeController controller;

		[SetUp]
		public void Setup()
		{
			employeeRepository = new Mock<IEmployeeRepository>();
			controller = new EmployeeController(employeeRepository.Object);
		}

		[Test]
		public void DeleteEmployee_PassId_RedirectToActionIsCalled()
		{
			var result =  controller.DeleteEmployee(1);

			Assert.That(result, Is.InstanceOf<ActionResult>());
		}

		[Test]
		public void DeleteEmployee_PassId_DeleteEmployeeFromRepositoryIsCalled()
		{
			controller.DeleteEmployee(1);
			
			employeeRepository.Verify(e => e.DeleteEmployee(1));
		}
	}
}