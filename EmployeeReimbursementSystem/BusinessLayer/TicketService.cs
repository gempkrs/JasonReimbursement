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
}

public class TicketService : ITicketService {
    // Dependency Injection
    private readonly ITicketRepository _itr;
    private readonly IEmployeeRepository _ier;
    private IEmployeeValidationService _ievs;
    public TicketService(ITicketRepository itr, IEmployeeRepository ier) {
        this._itr = itr;
        this._ier = ier;
        this._ievs = new EmployeeValidationService(this._ier);
    }
    // TODO
    public ReimburseTicket AddTicket(int empId, string reason, int amount, string description) {
        if(!_ievs.isEmployee(empId))
            return null!;
        // TODO Add ticket valdidation

        // TMP; until sql works... if we pass validation send data to repo layer for a query.
        List<ReimburseTicket> ticketDb = _itr.GetTickets();
        int ticketId = ticketDb.Count() + 1;
        ReimburseTicket newTicket = new ReimburseTicket(ticketId, empId, reason, amount, description);
        ticketDb.Add(newTicket);
        _itr.PostTickets(ticketDb);

        return newTicket;
    }
}