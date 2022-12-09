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
    public ReimburseTicket ApproveTicket(int empId, int tickId);
    public ReimburseTicket DenyTicket(int empId, int ticketId);
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
        if(!_ievs.isEmployee(empId) || !_itvs.ValidTicket(reason, amount, desc))
            return null!;

        // TODO, TMP; until sql works... if we pass validation send data to repo layer for a query.
        List<ReimburseTicket> ticketDb = _itr.GetTickets();
        int ticketId = ticketDb.Count() + 1;
        ReimburseTicket newTicket = new ReimburseTicket(ticketId, empId, reason, amount, desc);
        ticketDb.Add(newTicket);
        _itr.PostTickets(ticketDb);

        return newTicket;
    }

    public List<ReimburseTicket> GetPendingTickets(int empId) {
        // TODO, TMP; until sql works... in db, first check if employee is a manager; then query for tickets that are pending
        if(!_ievs.isManager(empId)) return null!;
        
        // tmp... with sql we will get list from repo layer(?)
        List<ReimburseTicket> pendingTickets = new List<ReimburseTicket>();
        foreach(ReimburseTicket ticket in _itr.GetTickets()) {
            if(ticket.status == 0) pendingTickets.Add(ticket);
        }

        return pendingTickets;
    }

    public ReimburseTicket ApproveTicket(int empId, int ticketId) {
        // TODO, Tmp; until sql works ... in db, check if employee is manager then check if ticket exists. Update ticket status
        if(!_ievs.isManager(empId) || !_itvs.isTicket(ticketId)) return null!;

        //tmp, with sql we will just do update query using ticketId...
        List<ReimburseTicket> ticketDb = _itr.GetTickets();
        foreach(ReimburseTicket ticket in ticketDb) {
            if(ticket.id == ticketId && ticket.status == 0) {
                if(ticket.employeeID == empId) return null!;
                ticket.status = 1;
                _itr.PostTickets(ticketDb);
                return ticket;
            }
        }

        return null!;
    }

    public ReimburseTicket DenyTicket(int empId, int ticketId) {
        // TODO, Tmp; until sql works ... in db, check if employee is manager then check if ticket exists. Update ticket status
        if(!_ievs.isManager(empId) || !_itvs.isTicket(ticketId)) return null!;

        //tmp, with sql we will just do update query using ticketId...
        List<ReimburseTicket> ticketDb = _itr.GetTickets();
        foreach(ReimburseTicket ticket in ticketDb) {
            if(ticket.id == ticketId && ticket.status == 0) {
                if(ticket.employeeID == empId) return null!; // TODO CHECK IF MANAGER IS TRYING TO APPROVE/DENY THEIR OWN TICKET
                ticket.status = 2;
                _itr.PostTickets(ticketDb);
                return ticket;
            }
        }

        return null!;
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