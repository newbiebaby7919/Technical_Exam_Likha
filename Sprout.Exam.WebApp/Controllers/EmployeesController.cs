using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Models;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.Business.Factories;
using Microsoft.EntityFrameworkCore;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await context.Employee.ToListAsync();
                return Ok(result);
            } 
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected error occurred. Error: " + e.Message);
            }

        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await Task.FromResult(context.Employee.FirstOrDefault(e => e.Id == id));
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound($"Employee with id '{id}' not found.");
            } 
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected error occurred. Error: " + e.Message);
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var item = await Task.FromResult(context.Employee.FirstOrDefault(e => e.Id == input.Id));
            if (item == null) return NotFound();
            item.FullName = input.FullName;
            item.Tin = input.Tin;
            item.Birthdate = input.Birthdate;
            item.TypeId = input.TypeId;

            if (item.FullName.Length == 0)
            {
                return BadRequest("Required!");
            }

            if (item.Tin.Length == 0)
            {
                return BadRequest("Required!");
            }

            if (item.Tin.Length != 9 && item.Tin.Length != 12)
            {
                return BadRequest("TIN should be 9 digits or 12 digits!");
            }


            context.Employee.Update(item);
            await context.SaveChangesAsync();

            return Ok(item);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            try
            {
                var employeeFactory = new EmployeeFactory();

                if (input.FullName.Length == 0)
                {
                    return BadRequest("Full name is required!");
                }

                if (input.Tin.Length == 0)
                {
                    return BadRequest("TIN is required!");
                }

                if (input.Tin.Length != 9 && input.Tin.Length != 12)
                {
                    return BadRequest("TIN should be 9 digits or 12 digits!");
                }

                var employee = employeeFactory.CreateEmployee(
                    input.FullName,
                    input.Birthdate,
                    input.Tin,
                    input.TypeId

                );

                await context.Employee.AddAsync(employee);
                await context.SaveChangesAsync();

                return Created($"/api/employees/{input.Tin}", 1);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unexpected error occurred. Error: " + e.Message);
            }
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Task.FromResult(context.Employee.FirstOrDefault(e => e.Id == id));
            if (result == null) return NotFound();
            context.Employee.Remove(result);
            await context.SaveChangesAsync();
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, [FromBody] CalculateRequest calculateRequest)
        {
            var result = await Task.FromResult(context.Employee.FirstOrDefault(e => e.Id == id));
            var employeeFactory = new EmployeeFactory();

            if (result == null) return NotFound();
            var type = (EmployeeType) result.TypeId;
            return type switch
            {
                EmployeeType.Regular =>
                    //create computation for regular.
                    Ok(employeeFactory.CreateRegularEmployee(result).CalculatedSalary(calculateRequest.Days)),
                EmployeeType.Contractual =>
                    //create computation for contractual.
                    Ok(employeeFactory.CreateContractualEmployee(result).CalculatedSalary(calculateRequest.Days)),
                _ => NotFound("Employee Type not found")
            };

        }

    }
}
