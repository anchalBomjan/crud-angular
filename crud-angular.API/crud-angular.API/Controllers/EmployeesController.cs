﻿using crud_angular.API.Models;
using crud_angular.API.Repositories;
using crud_angular.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crud_angular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<ActionResult>AddEmployee([FromBody] Employee model)
        {


            //await _repository.AddEmployeeAsync(model);
           // return Ok(); // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Return a BadRequest response with validation errors
                return BadRequest(ModelState);
            }

            try
            {
                // Add the employee using your repository or service
                await _repository.AddEmployeeAsync(model);

                // Return a successful response
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                // You might want to add more specific error handling here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the employee.");
            }


        }
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employeList = await _repository.GetAllEmployees();
            return Ok(employeList);
        }


        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById([FromRoute] int id)
        {
            var employee = await _repository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee([FromRoute] int id, [FromBody] Employee model)
        {
            await _repository.UpdateEmployeeAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
           await _repository.DeleteEmployeeAsync(id);
         

            return Ok();


        }


    }
} 
