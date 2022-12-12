using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ILoggerTicketRepository { // TODO
    /*
    ReimburseTicket PostTicket(string guid, string r, int a, string d, DateTime t, int eId);
    ReimburseTicket GetTicket(string ticketId);
    ReimburseTicket UpdateTicket(string ticketId, int statusId);
    List<ReimburseTicket> GetTickets(int employeeId);
    List<ReimburseTicket> GetTickets(int employeeId, int statusId);
    Queue<ReimburseTicket> GetPending(int managerId);
    */
        void LogTicketGet(bool success, object o);
        void LogTicketPost(bool success, object o);
        void LogTicketPut(bool success, object o);
    }

    public class LoggerTicketRepository : ILoggerTicketRepository {
        public void LogTicketGet(bool success, object o) {
            if(success)
                Console.WriteLine($"GET request for ReimburseTicket was successful using {o} argument");
            else
                Console.WriteLine($"GET request for ReimburseTicket FAILED using {o} argument");
        }

        public void LogTicketPost(bool success, object o) {
            if(success)
                Console.WriteLine($"POST request for ReimburseTicket was successful using {o} argument");
            else
                Console.WriteLine($"POST request for ReimburseTicket FAILED using {o} argument");
        }

        public void LogTicketPut(bool success, object o) {
            if(success)
                Console.WriteLine($"PUT request for ReimburseTicket was successful using {o} argument");
            else
                Console.WriteLine($"PUT request for ReimburseTicket FAILED using {o} argument");
        }
    }
}