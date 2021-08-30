using BenefitsSystem.Web.ViewModels;
using BenefitsSystem.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace BenefitsSystem.Web.Controllers
{
    public class BenefitsSystemController : Controller
    {
        private readonly IBenefitsSystemService benefitsSystemServices;
        private readonly ILogger<BenefitsSystemController> myLogger;
        public BenefitsSystemController(IBenefitsSystemService _benefitsSystemServices, ILogger<BenefitsSystemController> logger)
        {
            benefitsSystemServices = _benefitsSystemServices;
            myLogger = logger;

        }

        // GET: for initial load and search
        public async Task<IActionResult> Index(string searchName)
        {
            List<EmployeeViewModel> employeeList = null;
            try
            {
                if (!String.IsNullOrEmpty(searchName))
                    employeeList = await benefitsSystemServices.GetEmployeesAsync(searchName);
                else
                    employeeList = await benefitsSystemServices.GetEmployeesAsync();
            }
            catch (Exception ex)
            {
                myLogger.LogError(ex,$"Error loading list of employees. Search Name: {searchName}");
                
            }
            return View(employeeList);
        }

        // GET: BenefitsSystemController/Employee/5
        /// <summary>
        /// Get Employee Details including dependant info
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        [HttpGet("Employee/{id}"), ActionName("Employee")]
        public async Task<IActionResult> Employee(int id)
        {
            EmployeeViewModel employee = null;
            try
            {
                if (id <= 0)
                {
                    myLogger.LogError($"Unable to Get Employee. Invalid employee ID {id}.");
                    return BadRequest(id);
                }
                employee = await benefitsSystemServices.GetEmployeeDetailsAsync(id);

                if (employee == null)
                {
                    myLogger.LogError($"Unable to Get Employee.Employee ID {id} not found.");
                    return NotFound(id);
                }
            }
            catch (Exception ex)
            {
                myLogger.LogError(ex, $"Error getting employee details. ID: {id}");
            }
            return View(employee);

        }

        // GET: BenefitsSystemController/Dependants/5
        /// <summary>
        /// Get Employee Dependant Details
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        [HttpGet("Dependants/{id}"), ActionName("Dependants")]
        public async Task<IActionResult> Dependants(int id)
        {
            EmployeeViewModel employee = null;
            try
            {
                if (id <= 0)
                {
                    myLogger.LogError($"Unable to Get Dependants. Invalid employee ID {id}.");
                    return BadRequest(id);
                }
                employee = await benefitsSystemServices.GetEmployeeDependantsAsync(id);

                if (employee == null)
                {
                    myLogger.LogError($"Unable to Get Dependants.Employee ID {id} not found.");
                    return NotFound(id);
                }
            }
            catch (Exception ex)
            {
                myLogger.LogError(ex, $"Error getting dependants details. ID: {id}");
            }
            return View(employee);

        }
        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteEmployee")]
        public async Task<ActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    myLogger.LogError($"Unable to Delete Employee. Invalid employee ID {id}.");
                    return BadRequest(id);
                }
                await benefitsSystemServices.DeleteEmployeeAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                myLogger.LogError(ex, $"Error deleting employee. ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected Error while deleting employee: " + ex.Message);
            }
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="newEmployee">Employee information</param>
        /// <returns></returns>
        [HttpPost, ActionName("AddEmployee")]
        public async Task<ActionResult> AddEmployeeAsync(EmployeeViewModel newEmployee)
        {
            try
            {
                int newid = await benefitsSystemServices.AddEmployeeAsync(newEmployee);
                return Json(new { id = newid });
            }
            catch (Exception ex)
            {
                myLogger.LogError(ex, $"Error adding employee.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Add Dependant 
        /// </summary>
        /// <param name="newDependant">Dependant information including associated employee ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("AddDependant")]
        public async Task<ActionResult> AddDependantAsync(DependantViewModel newDependant)
        {
            try
            {
                if (newDependant.EmployeeID <= 0)
                    return BadRequest();
                int newid = await benefitsSystemServices.AddDependantAsync(newDependant);
                return Json(new { id = newid });

            }
            catch (Exception ex)
            {
                myLogger.LogError(ex, $"Error adding dependant.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Delete Dependant
        /// </summary>
        /// <param name="id">Dependant ID</param>
        /// <returns></returns>
        // POST: BenefitsSystemController/DeleteDependant/5
        [HttpPost, ActionName("DeleteDependant")]
        public async Task<ActionResult> DeleteDependant(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid dependant ID!");
                
                await benefitsSystemServices.DeleteDependantAsync(id);
                return Ok();
                
            }
            catch (Exception ex)
            {
                myLogger.LogError(ex, $"Error deleting dependant.{id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected Error while deleting dependant: " + ex.Message);
            }
        }

    }
}
