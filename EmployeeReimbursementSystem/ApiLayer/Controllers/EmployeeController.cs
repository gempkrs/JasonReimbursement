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
    // Dependency Injection for Employee Service class
    private readonly IEmployeeService _ies;
    public EmployeeController(IEmployeeService ies) => this._ies = ies;
        
    [HttpPost("RegisterEmployee")]
    public ActionResult<Employee> RegisterEmployee(string email, string password) {
        Employee newEmployee = _ies.RegisterEmployee(email, password);
        return Created("path/to/db", newEmployee);
    }
}