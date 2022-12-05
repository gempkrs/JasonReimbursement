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

    [HttpPost("RegisterManager")]
    public ActionResult<Employee> RegisterEmployee(string email, string password, int roleid) {
        Employee newEmployee = _ies.RegisterEmployee(email, password, roleid);
        return Created("path/to/db", newEmployee);
    }

    [HttpGet("LoginEmployee")]
    public ActionResult<Employee> LoginEmployee(string email, string password) {
        Employee employee = _ies.LoginEmployee(email, password);
        return Created("path/", employee);
    }

    [HttpPut("EditPassword")]
    public ActionResult<Employee> EditPassword(int empId, string oldPassword, string newPassword) {
        Employee employee = _ies.EditEmployee(empId, oldPassword, newPassword);
        return Created("path/", employee);
    }

    [HttpPut("EditEmail")]
    public ActionResult<Employee> EditEmail(int empId, string email) {
        Employee employee = _ies.EditEmployee(empId, email);
        return Created("path/", employee);
    }

    [HttpPut("EditRole")]
    public ActionResult<Employee> EditRole(int empId, int roleId) {
        Employee employee = _ies.EditEmployee(empId, roleId);
        return Created("path/", employee);
    }
}