using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ModelLayer;
using BusinessLayer;

namespace ApiLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReimburseTicketController : ControllerBase
    {
        // Dependency injection for ticket service class
        private readonly ITicketService _its;
        public ReimburseTicketController(ITicketService its) => this._its = its;

        [HttpPost("ReimbursementTicket")]
        public ActionResult<ReimburseTicket> ReimbursementTicket(int empId, string reason, double amount, string description) {
            ReimburseTicket ticket = _its.AddTicket(empId, reason, amount, description);
            return Created("path/", ticket);
        }

        [HttpGet("PendingTickets")]
        public ActionResult<Queue<ReimburseTicket>> PendingTickets(int managerId) {
            Queue<ReimburseTicket> pendingTickets = _its.GetPendingTickets(managerId);
            return Created("path/", pendingTickets);
        }

        [HttpPut("Approve")]
        public ActionResult<ReimburseTicket> Approve(int managerId, string ticketId) {
            ReimburseTicket approvedTicket = _its.ApproveTicket(managerId, ticketId);
            return Created("path/", approvedTicket);
        }

        [HttpPut("Deny")]
        public ActionResult<ReimburseTicket> Deny(int managerId, string ticketId) {
            ReimburseTicket deniedTicket = _its.DenyTicket(managerId, ticketId);
            return Created("path/", deniedTicket);
        }
    }
}