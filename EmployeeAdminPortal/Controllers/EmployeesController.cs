using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
	[Route("api/[controller]")] // http://localhost:port/api/Employees
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		// get all employees
        [HttpGet]
		public IActionResult GetAllEmployee()
		{
			var employees = dbContext.Employees.ToList();
			return Ok(employees);
		}

		// get employee by id
		[HttpGet]
		[Route("{id:guid}")]
		public IActionResult GetEmployeeById(Guid id)
		{
			var employee = dbContext.Employees.FirstOrDefault(u => u.Id == id);

			if(employee == null)
			{
				return NotFound();
			}

			return Ok(employee);
		}
		

		// create new employee
		[HttpPost]
		public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
		{
			var employeeEntity = new Employee()
			{
				Name = addEmployeeDto.Name,
				Email = addEmployeeDto.Email,
				Phone = addEmployeeDto.Phone,
				Salary = addEmployeeDto.Salary
			};

			dbContext.Employees.Add(employeeEntity);
			dbContext.SaveChanges();

			return Ok(employeeEntity);
		}

		// update employee
		[HttpPut]
		[Route("{id:guid}")]
		public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
		{
			var employee = dbContext.Employees.FirstOrDefault(u => u.Id == id);

			if(employee == null)
			{
				return NotFound();
			}

			employee.Name = updateEmployeeDto.Name;
			employee.Email = updateEmployeeDto.Email;
			employee.Phone = updateEmployeeDto.Phone;
			employee.Salary = updateEmployeeDto.Salary;

			dbContext.SaveChanges();

			return Ok(employee);
		}

		// delete employee
		[HttpDelete]
		[Route("{id:guid}")]
		public IActionResult DeleteEmployee(Guid id)
		{
			var employee = dbContext.Employees.FirstOrDefault(u => u.Id == id);

			if (employee == null)
			{
				return NotFound();
			}

			dbContext.Employees.Remove(employee);
			dbContext.SaveChanges();

			return Ok("Record with ID: " + id + " was deleted succefully");
		}
	}
}
