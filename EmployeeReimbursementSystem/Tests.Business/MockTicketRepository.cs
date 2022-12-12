/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Import necessary layers
using RepositoryLayer;
using ModelLayer;

namespace Tests.Business
{
    public class MockTicketRepository : ITicketRepository
    {
        private List<ReimburseTicket> ticketDb = new List<ReimburseTicket>();
        public MockTicketRepository() {
            ReimburseTicket t1 = new ReimburseTicket(
                "t1",
                "Valid",
                1000,
                "A description",
                0,
                DateTime.Now,
                1
            );
            ReimburseTicket t2 = new ReimburseTicket(
                "t2",
                "Valid 2",
                1000,
                "Another one",
                0,
                DateTime.Now,
                1
            );
            ReimburseTicket t3 = new ReimburseTicket(
                "t3",
                "Approved",
                1000,
                "Another one",
                1,
                DateTime.Now,
                1
            );
            ticketDb.Add(t1);
            ticketDb.Add(t2);
            ticketDb.Add(t3);
        }

        public Queue<ReimburseTicket> GetPending(int managerId)
        {
            throw new NotImplementedException();
        }

        public ReimburseTicket GetTicket(string ticketId)
        {
            foreach(ReimburseTicket t in ticketDb) {
                if(t.guid!.Equals(ticketId))
                    return t;
            }
            return null!;
        }

        public List<ReimburseTicket> GetTickets(int employeeId)
        {
            throw new NotImplementedException();
        }

        public List<ReimburseTicket> GetTickets(int employeeId, int statusId)
        {
            throw new NotImplementedException();
        }

        public ReimburseTicket PostTicket(string guid, string r, int a, string d, DateTime t, int eId)
        {
            throw new NotImplementedException();
        }

        public ReimburseTicket UpdateTicket(string ticketId, int statusId)
        {
            throw new NotImplementedException();
        }
    }
}