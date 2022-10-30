using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMCrud.IRepositories;
using EmployeeMCrud.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeMCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController(IDepartmentRepository department)
        {
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            StatusResult<IEnumerable<Department>> obj = new StatusResult<IEnumerable<Department>>();
            obj.Message="Fetched Successfully";
            obj.Status="FETCHED";
            obj.Result = await _department.GetDepartment();
            //return Ok(await _department.GetDepartment());
            return Ok(obj);
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            StatusResult<Department> obj = new StatusResult<Department>();
            obj.Message = "Successfull fetched.";
            obj.Status="OK";
            obj.Result = await _department.GetDepartmentByID(Id);
            //return Ok(await _department.GetDepartmentByID(Id));
            return Ok(obj);
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<ActionResult> Post(Department dep)
        {
            var result = await _department.InsertDepartment(dep);

            if (result.DepartmentId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            //return Ok("Added Successfully");
            StatusResult<string> obj = new StatusResult<string>();
            obj.Message="Added Successfully";
            obj.Status="CREATED";
            return StatusCode(StatusCodes.Status201Created,obj);
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(Department dep)
        {
            StatusResult<string> obj =new StatusResult<string>();
            await _department.UpdateDepartment(dep);
            obj.Message="Updated Successfully";
            obj.Status="SUCCESS";
            return Ok(obj);
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteDepartment")]
        public async Task<IActionResult> Delete(int id)
        {
            _department.DeleteDepartment(id);
            StatusResult<string> obj = new StatusResult<string>();
            obj.Message="Deleted Successfully";
            obj.Status="SUCCESS";
            //return new JsonResult("Deleted Successfully");
            return Ok(obj);
        }
    }
}

