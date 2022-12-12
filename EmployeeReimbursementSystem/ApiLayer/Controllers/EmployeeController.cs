using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ModelLayer;
using BusinessLayer;

namespace ApiLayer.Controllers;
    

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase {
    // Dependency Injection for Employee Service class and Ticket Service class
    private readonly IEmployeeService _ies;
    private readonly ITicketService _its;
    public EmployeeController(IEmployeeService ies, ITicketService its) {
        this._ies = ies;
        this._its = its;
    }
        
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

    [HttpGet("EmployeeTickets")]
    public ActionResult<List<ReimburseTicket>> EmployeeTickets(int empId) {
        List<ReimburseTicket> employeeTickets = _its.GetEmployeeTickets(empId);
        return Created("path/", employeeTickets);
    }

    [HttpGet("EmployeeTicketsByStatus")]
    public ActionResult<List<ReimburseTicket>> EmployeeTickets(int empId, int status) {
        List<ReimburseTicket> employeeTickets = _its.GetEmployeeTickets(empId, status);
        return Created("path/", employeeTickets);
    }
}