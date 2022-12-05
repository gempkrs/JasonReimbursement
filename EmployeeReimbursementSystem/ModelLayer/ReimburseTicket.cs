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
    public int id{get; set;}
    public int employeeID{get; set;}
    public string reason{get; set;}
    public int amount{get; set;}
    public string description{get; set;}

    // 0 is pending, 1 is approved, 2 is rejected
    public int status{get; set;}

    // Constructor to be used when an employee creates a ticket
    public ReimburseTicket(int id, int employeeID, string reason, int amount, string description) {
        this.id = id;
        this.employeeID = employeeID;
        this.reason = reason;
        this.amount = amount;
        this.description = description;
        this.status = 0;
    }

    public ReimburseTicket() {}
}
