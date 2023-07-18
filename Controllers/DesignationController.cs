// DesignationController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeDemoProject.IRepositories;
using EmployeeDemoProject.Models;
using EmployeeDemoProject.Repositories;
using EmployeeMCrud.IRepositories;
using EmployeeMCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _designation;
        public DesignationController(IDesignationRepository department)
        {
            _designation = department ??
                throw new ArgumentNullException(nameof(department));
        }

        [HttpGet]
        [Route("GetDesignations")]
        public async Task<IActionResult> Get()
        {
            StatusResult<IEnumerable<Designation>> obj = new StatusResult<IEnumerable<Designation>>();
            //obj.Message = "Fetched Successfully";
            //obj.Status = "FETCHED";
            //obj.Result = await _designation.GetDesignations();
            return Ok(await _designation.GetDesignations());
            //return Ok(obj);
        }


        [HttpGet("{designationName}")]

        public async Task<ActionResult<Designation>> GetDesignationsByName(string designationName)
        {
            var designation = await _designation.GetDesignationByName(designationName);
            if (designation == null)
            {
                return NotFound();
            }
            return Ok(designation);
        }


        [HttpPost]
        [Route("CreateDesignation")]
        public async Task<IActionResult> CreateDesignation([FromBody] Designation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDesignation = await _designation.GetDesignationByName(designation.DesignationName);
            if (existingDesignation != null)
            {
                return Conflict("Designation already exists.");
            }

            var createdDesignation = await _designation.InsertDesignation(designation);

            if (createdDesignation == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create designation");
            }

            var response = new
            {
                Message = "Created Successfully",
                Status = "CREATED",
                Designation = createdDesignation
            };

            return StatusCode(StatusCodes.Status201Created, response);
        }
      

    }

}



