using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMCrud.IRepositories;
using EmployeeMCrud.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        private IHostingEnvironment _environment;
        public EmployeeController(IEmployeeRepository employee, IDepartmentRepository department, IHostingEnvironment environment)
        {
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            _department = department ??
                throw new ArgumentNullException(nameof(department));
            _environment = environment;
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            StatusResult<IEnumerable<Employee>> obj = new StatusResult<IEnumerable<Employee>>();
            obj.Message = "Fetched Successfully";
            obj.Status= "Fetched";
            obj.Result = await _employee.GetEmployees();
            /*return Ok(await _employee.GetEmployees());*/
            return Ok(obj);

        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            StatusResult<Employee> obj = new StatusResult<Employee>();
            obj.Message = "Fetched Successfully";
            obj.Status= "Fetched";
            obj.Result = await _employee.GetEmployeeByID(Id);
            /*return Ok(await _employee.GetEmployeeByID(Id));*/
            return Ok(obj);
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {
            var result = await _employee.InsertEmployee(emp);
            if (result.EmployeeID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            StatusResult<string> obj = new StatusResult<string>();
            obj.Message = "Created Successfully";
            obj.Status= "SUCCESS";
            obj.Result = "1";
            /*return Ok("Added Successfully");*/
            return Ok(obj);
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
            StatusResult<Employee> obj = new StatusResult<Employee>();
            obj.Message = "Updated Successfully";
            obj.Status= "SUCCESS";
            obj.Result = await _employee.UpdateEmployee(emp);
            await _employee.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        //[HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _employee.DeleteEmployee(id);
            StatusResult<bool> obj = new StatusResult<bool>();
            obj.Message = "Deleted Successfully";
            obj.Status= "SUCCESS";
            obj.Result = result;
            //return new JsonResult("Deleted Successfully");
            return new JsonResult(obj);
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _environment.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    stream.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            StatusResult<IEnumerable<Department>> obj = new StatusResult<IEnumerable<Department>>();
            obj.Message = "Fetched Successfully";
            obj.Status= "FETCHED";
            obj.Result = await _department.GetDepartment();
            //return Ok(await _department.GetDepartment());
            return Ok(obj);
        }
    }
}

