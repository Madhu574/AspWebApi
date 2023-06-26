using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMCrud.IRepositories;
using EmployeeMCrud.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;



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
            //obj.Message = "Fetched Successfully";
            //obj.Status = "Fetched";
            //obj.Result = await _employee.GetEmployees();
            return Ok(await _employee.GetEmployees());
            //return Ok(obj);

        }
        //[HttpGet]
        //[Route("GetEmployeeByID/{Id}")]
        //public async Task<IActionResult> GetEmpByID(int Id)
        //{
        //    StatusResult<Employee> obj = new StatusResult<Employee>();
        //    //obj.Message = "Fetched Successfully";
        //    //obj.Status= "Fetched";
        //    //obj.Result = await _employee.GetEmployeeByID(Id);
        //    return Ok(await _employee.GetEmployeeByID(Id));
        //    //return Ok(obj);
        //}
        [HttpPost]
        [HttpPut]
        [Route("AddEditEmployee")]
        public async Task<IActionResult> AddEditEmployee(Employee emp)
        {
            StatusResult<string> obj = new StatusResult<string>();

            if (emp.EmployeeId == 0)
            {
                // Insert operation
                var insertResult = await _employee.InsertEmployee(emp);
                if (insertResult.EmployeeId == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }

                obj.Message = "Created Successfully";
                obj.Status = "SUCCESS";
                obj.Result = "1";
            }
            else
            {
                // Update operation
                var updateResult = await _employee.UpdateEmployee(emp);

                if (updateResult == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }

                obj.Message = "Updated Successfully";
                obj.Status = "SUCCESS";
                obj.Result = "1";
            }

            return Ok(obj);
        }
        //[HttpPost]
        //[HttpPut]
        //[Route("AddEditEmployee")]
        //public async Task<IActionResult> AddEditEmployee(Employee emp)
        //{
        //    StatusResult<string> obj = new StatusResult<string>();

        //    if (emp.EmployeeId == 0)
        //    {
        //        var result = await _employee.InsertEmployee(emp);

        //        if (result.EmployeeId == 0)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
        //        }

        //        obj.Message = "Added Successfully";
        //        //obj.Status = "CREATED";
        //        return StatusCode(StatusCodes.Status201Created, obj);
        //    }
        //    else
        //    {
        //        await _employee.UpdateEmployee(emp);

        //        obj.Message = "Updated Successfully";
        //        //obj.Status = "SUCCESS";
        //        return Ok(obj);
        //    }
        //}



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

