using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Importing the necessary layers
using ModelLayer;
using BusinessLayer;

namespace ApiLayer.Controllers;
    

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase {
    // Dependency Injection
    private readonly IEmployeeService _iemps;
    public EmployeeController(IEmployeeService iemps) => this._iemps = iemps;
        
    [HttpPost("AddEmployee")]
    public ActionResult<Employee> RegisterEmployee(string email, string password) {
        Employee newEmployee = _iemps.RegisterEmployee(email, password);
        return Created("path/to/db", newEmployee);
    }
}