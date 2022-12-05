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
}

public class TicketValidationService : ITicketValidationService {
    #region // Ticket Input Validation
    public bool ValidTicket(string reason, int amount, string desc) => ValidReason(reason) && ValidAmount(amount) && ValidDescription(desc);
    public bool ValidReason(string reason) => reason.Length > 1;
    public bool ValidDescription(string description) => description.Length > 1;
    public bool ValidAmount(int amount) => (amount > 0 && amount < 10000);
    #endregion
}