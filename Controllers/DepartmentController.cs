using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmployeeMCrud.IRepositories;
using EmployeeMCrud.Models;
using Microsoft.AspNetCore.Mvc;



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
            obj.Message = "Fetched Successfully";
            obj.Status = "FETCHED";
            obj.Result = await _department.GetDepartment();
            //return Ok(await _department.GetDepartment());
            return Ok(obj);
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
              StatusResult<Department> obj = new StatusResult<Department>();
             //obj.Message = "Successfull fetched.";
             //   obj.Status = "OK";
             //   obj.Result = await _department.GetDepartmentByID(Id);
            return Ok(await _department.GetDepartmentByID(Id));
               //return Ok(obj);
            

        }


        [HttpPost]
        [HttpPut]
        [Route("AddEditDepartment")]
        public async Task<IActionResult> AddEditDepartment(Department dep)
        {
            StatusResult<string> obj = new StatusResult<string>();

            if (dep.DepartmentId == 0)
            {
                var result = await _department.InsertDepartment(dep);

                if (result.DepartmentId == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }

                obj.Message = "Added Successfully";
                obj.Status = "CREATED";
                return StatusCode(StatusCodes.Status201Created, obj);
            }
            else
            {
                await _department.UpdateDepartment(dep);

                obj.Message = "Updated Successfully";
                obj.Status = "SUCCESS";
                return Ok(obj);
            }
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

