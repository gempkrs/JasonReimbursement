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

public class TicketService : ITicketService {
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
    
    public ReimburseTicket AddTicket(int empId, string reason, int amount, string desc) {
        if(!_ievs.isEmployee(empId) || !_itvs.ValidTicket(reason, amount, desc)) {
            Console.WriteLine("Invalid employeeId, or your ticket was invalid.");
            return null!;
        }
        return _itr.PostTicket(Guid.NewGuid().ToString(), reason, amount, desc, empId);
    }

    public List<ReimburseTicket> GetPendingTickets(int empId) {
        // TODO, TMP; until sql works... in db, first check if employee is a manager; then query for tickets that are pending
        if(!_ievs.isManager(empId)) {
            Console.WriteLine("Employee does not exist or have the righ permissions");
            return null!;
        } 
        
        // tmp... with sql we will get list from repo layer(?)
        // List<ReimburseTicket> pendingTickets = new List<ReimburseTicket>();
        // foreach(ReimburseTicket ticket in _itr.GetTickets()) {
        //     if(ticket.status == 0) pendingTickets.Add(ticket);
        // }

        return null!;
    }

    public ReimburseTicket ApproveTicket(int empId, string ticketId) {
        // TODO, Tmp; until sql works ... in db, check if employee is manager then check if ticket exists. Update ticket status
        if(!_ievs.isManager(empId) || !_itvs.ValidStatusChange(empId, ticketId)){
            Console.WriteLine("Invalid manager Id, manager is trying to edit their own ticket, or ticket doesn't exist");
            return null!;
        } 

        // //tmp, with sql we will just do update query using ticketId...
        // List<ReimburseTicket> ticketDb = _itr.GetTickets();
        // foreach(ReimburseTicket ticket in ticketDb) {
        //     if(ticket.id == ticketId && ticket.status == 0) {
        //         if(ticket.employeeID == empId) return null!;
        //         ticket.status = 1;
        //         _itr.PostTickets(ticketDb);
        //         return ticket;
        //     }
        // }

        return _itr.UpdateTicket(ticketId, 1);
    }

    public ReimburseTicket DenyTicket(int empId, string ticketId) {
        // TODO, Tmp; until sql works ... in db, check if employee is manager then check if ticket exists. Update ticket status
        if(!_ievs.isManager(empId) || !_itvs.ValidStatusChange(empId, ticketId)){
            Console.WriteLine("Invalid manager Id, manager is trying to edit their own ticket, or ticket doesn't exist");
            return null!;
        } 

        // //tmp, with sql we will just do update query using ticketId...
        // List<ReimburseTicket> ticketDb = _itr.GetTickets();
        // foreach(ReimburseTicket ticket in ticketDb) {
        //     if(ticket.id == ticketId && ticket.status == 0) {
        //         if(ticket.employeeID == empId) return null!;
        //         ticket.status = 2;
        //         _itr.PostTickets(ticketDb);
        //         return ticket;
        //     }
        // }

        return _itr.UpdateTicket(ticketId, 2);
    }

    public List<ReimburseTicket> GetEmployeeTickets(int empId) {
        // TODO, Tmp; until sql works... in db, check if employee exists and if they do, use their id to query their tickets.
        if(!_ievs.isEmployee(empId)) return null!;

        // Tmp, with sql we will do a query using the employee id.
        List<ReimburseTicket> employeeTickets = new List<ReimburseTicket>();
        foreach(ReimburseTicket ticket in _itr.GetTickets()) {
            if(ticket.employeeID == empId) employeeTickets.Add(ticket);
        }

        return employeeTickets;
    }

    public List<ReimburseTicket> GetEmployeeTickets(int empId, int status) {
        // TODO, Tmp; until sql works... in db, check if employee exists and if they do, use their id to query their tickets.
        if(!_ievs.isEmployee(empId)) return null!;

        // Tmp, with sql we will do a query using the employee id.
        List<ReimburseTicket> employeeTickets = new List<ReimburseTicket>();
        foreach(ReimburseTicket ticket in _itr.GetTickets()) {
            if(ticket.employeeID == empId && ticket.status == status) employeeTickets.Add(ticket);
        }
        
        return employeeTickets;
    }
}