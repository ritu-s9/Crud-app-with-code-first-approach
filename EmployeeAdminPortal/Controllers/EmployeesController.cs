using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContextcs dbContext;

        public EmployeesController(ApplicationDbContextcs dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {

            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);

        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDTO addEmployeeDTO) {

            var employeeEntity = new Employee() {
                Email = addEmployeeDTO.Email,
                Phone = addEmployeeDTO.Phone,
                Name = addEmployeeDTO.Name,
                salary = addEmployeeDTO.salary


            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();


            return Ok(employeeEntity);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id) {
            var employee = dbContext.Employees.Find(id);

            if (employee == null) {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
               
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployeeById(Guid id,UpdateEmployeeDTOcs updateEmployeeDTOcs) {
        
           var updateEmployee =  dbContext.Employees.Find(id);

            if (updateEmployee == null) {
                return NotFound();
            }
            else
            {
                updateEmployee.Email=updateEmployeeDTOcs.Email;
                updateEmployee.Phone = updateEmployeeDTOcs.Phone;
                updateEmployee.Name = updateEmployeeDTOcs.Name;
                updateEmployee.salary = (decimal)updateEmployeeDTOcs.salary;

                dbContext.SaveChanges();

                return Ok(updateEmployee);

            }
        
        
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmpById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
                return Ok("successfully delete");
            }

        }
    }
}
