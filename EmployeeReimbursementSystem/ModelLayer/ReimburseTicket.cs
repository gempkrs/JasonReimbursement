/* JASON TEJADA    PROJECT 1 MODEL LAYER CLASS    REVATURE
 * Desc:
 *        This class defines the Ticket object. Data fields are: ID, 
 *        employee ID, reason, amount, description, and status.
 *        Procedures will be defined in the business layer.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer;

public class ReimburseTicket {
    public string ?guid{get;set;}
    public int employeeID{get; set;}
    public string ?reason{get; set;}
    public double amount{get; set;}
    public string ?description{get; set;}
    public DateTime timeMade{get; set;}

    // 0 is pending, 1 is approved, 2 is rejected
    public int status{get; set;}

    // Constructor to be used when an employee creates a ticket
    public ReimburseTicket(string guid, string reason, double amount, string description, int statusId, DateTime timeMade, int employeeID) {
        this.guid = guid;
        this.reason = reason;
        this.amount = amount;
        this.description = description;
        this.status = statusId;
        this.timeMade = timeMade;
        this.employeeID = employeeID;
    }
    public ReimburseTicket() {}
}
