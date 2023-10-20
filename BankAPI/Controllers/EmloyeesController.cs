using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmloyeesController : Controller
    {
        EmployeeService employeeService = new EmployeeService();

        //получения сотрудника по идентификатору,
        [HttpGet("{EmployeeId}")]
        public IActionResult GetEmployee(int EmployeeId)
        {
            Employee employee = employeeService.GetEmployee(EmployeeId);

            if (employee == null)
            {
                return NotFound($"Employee with ID {EmployeeId} not found");
            }
            return Ok(employee); // Вернуть успешный статус HTTP
        }

        //добавления нового сотрудника,
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            employeeService.AddEmployee(employee);
            return Ok();
        }

        //изменение сотрудника по идентификатору,
        [HttpPatch("{EmployeeId}")]
        public IActionResult UpdateEmployee(int EmployeeId, [FromBody] Employee employee)
        {
            employeeService.UpdateEmployee(EmployeeId, employee);
            return Ok();
        }

        //удаления сотрудника,
        [HttpDelete("{EmployeeId}")]
        public IActionResult RemoveEmployee(int EmployeeId)
        {
            Employee employee = employeeService.GetEmployee(EmployeeId);

            if (employee != null)
            {
                employeeService.RemoveEmployee(EmployeeId);
                return Ok();
            }
            else
            {
                return NotFound($"Employee with ID {EmployeeId} not found");
            }
        }
    }
}
