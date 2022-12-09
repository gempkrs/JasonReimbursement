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
    public ActionResult<Employee> PostEmployee(string email, string password) {
        Employee newEmployee = _ies.PostEmployee(email, password);
        return Created("path/to/db", newEmployee);
    }

    [HttpPost("RegisterManager")]
    public ActionResult<Employee> PostEmployee(string email, string password, int roleid) {
        Employee newEmployee = _ies.PostEmployee(email, password, roleid);
        return Created("path/to/db", newEmployee);
    }

    [HttpGet("LoginEmployee")]
    public ActionResult<Employee> LoginEmployee(string email, string password) {
        Employee employee = _ies.LoginEmployee(email, password);
        return Created("path/", employee);
    }

    [HttpPut("ChangePassword")]
    public ActionResult<Employee> EditEmployee(int targetId, string oldPassword, string newPassword) {
        Employee employee = _ies.EditEmployee(targetId, oldPassword, newPassword);
        return Created("path/", employee);
    }

    [HttpPut("ChangeEmail")]
    public ActionResult<Employee> EditEmployee(int targetId, string newEmail) {
        Employee employee = _ies.EditEmployee(targetId, newEmail);
        return Created("path/", employee);
    }

    [HttpPut("ChangeRole")]
    public ActionResult<Employee> EditEmployee(int managerId, int targetId, int newRoleId) {
        Employee employee = _ies.EditEmployee(managerId, targetId, newRoleId);
        return Created("path/", employee);
    }
}