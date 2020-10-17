using System.Data.Entity;

namespace TestNinja.Mocking
{
	public class EmployeeController
	{
		private readonly IEmployeeRepository employeeRepository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		}

		public ActionResult DeleteEmployee(int id)
		{
			employeeRepository.DeleteEmployee(id);

			return RedirectToAction("Employees");
		}

		private ActionResult RedirectToAction(string employees)
		{
			return new RedirectResult();
		}
	}

	public class ActionResult { }

	public class RedirectResult : ActionResult { }

	public class Employee
	{
	}
}