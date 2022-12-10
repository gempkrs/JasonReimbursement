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
public interface ITicketValidationService {
    public bool ValidReason(string reason);
    public bool ValidAmount(int amount);
    public bool ValidDescription(string description);
    public bool ValidTicket(string reason, int amount, string description);
    public bool isTicket(int ticketId);
}

public class TicketValidationService : ITicketValidationService {
    private readonly ITicketRepository _itr;
    public TicketValidationService(ITicketRepository itr) => this._itr = itr;
    #region // Ticket Input Validation
    public bool ValidTicket(string reason, int amount, string desc) => ValidReason(reason) && ValidAmount(amount) && ValidDescription(desc);
    public bool ValidReason(string reason) => reason.Length > 1;
    public bool ValidDescription(string description) => description.Length > 1;
    public bool ValidAmount(int amount) => (amount > 0 && amount < 10000);
    #endregion

    // TODO? Might not need this anymore.
    // TODO Make this work with SQL
    public bool isTicket(int ticketId) {
        return false;
    }
}