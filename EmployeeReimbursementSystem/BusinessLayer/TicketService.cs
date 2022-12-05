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
    // Dependency Injection for repository Layer

    // TODO
    public ReimburseTicket AddTicket(int empId, string reason, int amount, string description) {
        return null!;
    }
}