/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Import our necessary layers
using ModelLayer;
using RepositoryLayer;

namespace BusinessLayer;

public interface ITicketService { 
    public ReimburseTicket AddTicket(int empId, string reason, int amount, string description);
    public List<ReimburseTicket> GetPendingTickets(int empId);
    public ReimburseTicket ApproveTicket(int empId, string tickId);
    public ReimburseTicket DenyTicket(int empId, string ticketId);
    public List<ReimburseTicket> GetEmployeeTickets(int empId);
    public List<ReimburseTicket> GetEmployeeTickets(int empId, int status);
}

public class TicketService : ITicketService { // TODO Refactor to work with logger
    // Dependency Injection
    private readonly ITicketRepository _itr;
    private readonly IEmployeeRepository _ier;
    private IEmployeeValidationService _ievs;
    private ITicketValidationService _itvs;
    public TicketService(ITicketRepository itr, IEmployeeRepository ier) {
        this._itr = itr;
        this._ier = ier;
        this._ievs = new EmployeeValidationService(this._ier);
        this._itvs = new TicketValidationService(this._itr);
    }
    
    public ReimburseTicket AddTicket(int empId, string reason, int amount, string desc) { // TODO refactor to create ticket with datetime
        if(!_ievs.isEmployee(empId) || !_itvs.ValidTicket(reason, amount, desc)) {
            Console.WriteLine("Invalid employeeId, or your ticket was invalid.");
            return null!;
        }
        return _itr.PostTicket(Guid.NewGuid().ToString(), reason, amount, desc, empId);
    }

    public List<ReimburseTicket> GetPendingTickets(int managerId) { // TODO Refactor to return a queue, ordered by the time the tickets were submitted
        if(!_ievs.isManager(managerId)) {
            Console.WriteLine("Employee does not exist or have the righ permissions");
            return null!;
        } 
        
        return _itr.GetPending(managerId);
    }

    public ReimburseTicket ApproveTicket(int empId, string ticketId) {
        if(!_ievs.isManager(empId) || !_itvs.ValidStatusChange(empId, ticketId)){
            Console.WriteLine("Invalid manager Id, manager is trying edit an invalid ticket, or ticket doesn't exist");
            return null!;
        } 

        return _itr.UpdateTicket(ticketId, 1);
    }

    public ReimburseTicket DenyTicket(int empId, string ticketId) {
        if(!_ievs.isManager(empId) || !_itvs.ValidStatusChange(empId, ticketId)){
            Console.WriteLine("Invalid manager Id, manager is trying to edit an invalid ticket, or ticket doesn't exist");
            return null!;
        } 

        return _itr.UpdateTicket(ticketId, 2);
    }

    public List<ReimburseTicket> GetEmployeeTickets(int empId) {
        if(!_ievs.isEmployee(empId)) {
            Console.WriteLine("Invalid employeeId");
            return null!;
        } 

        return _itr.GetTickets(empId);
    }

    public List<ReimburseTicket> GetEmployeeTickets(int empId, int status) {
        if(!_ievs.isEmployee(empId)) {
            Console.WriteLine("Invalid employeeId");
            return null!;
        }
    
        return _itr.GetTickets(empId, status);
    }
}