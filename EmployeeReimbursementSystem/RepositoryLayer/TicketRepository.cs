/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

// Importing necessary layers
using ModelLayer;

namespace RepositoryLayer;

public interface ITicketRepository {
    List<ReimburseTicket> GetTickets();
    void PostTickets(List<ReimburseTicket> ticketDB);
}

public class TicketRepository : ITicketRepository {
    public List<ReimburseTicket> GetTickets() {
        if(File.Exists("TicketDatabase.json")) {
            return JsonSerializer.Deserialize<List<ReimburseTicket>>(File.ReadAllText("TicketDatabase.json"))!;
        } else {
            return new List<ReimburseTicket>();
        }
    }

    public void PostTickets(List<ReimburseTicket> ticketDb) {
        string serializedDb = JsonSerializer.Serialize(ticketDb);
        File.WriteAllText("TicketDatabase.json", serializedDb);
    }
}