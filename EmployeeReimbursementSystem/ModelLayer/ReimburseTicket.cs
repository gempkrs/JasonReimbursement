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
    // TODO get rid of the id field
    //public int id{get; set;}
    public string ?guid{get;set;}
    public int employeeID{get; set;}
    public string ?reason{get; set;}
    public int amount{get; set;}
    public string ?description{get; set;}

    // 0 is pending, 1 is approved, 2 is rejected
    public int status{get; set;}

    // Constructor to be used when an employee creates a ticket
    public ReimburseTicket(string guid, string reason, int amount, string description, int statusId, int employeeID) {
        this.guid = guid;
        this.reason = reason;
        this.amount = amount;
        this.description = description;
        this.status = statusId;
        this.employeeID = employeeID;
    }

    // TODO Deprecate...
    // public ReimburseTicket(int id, string reason, int amount, string description, int statusId, int employeeID) {
    //     this.id = id;
    //     this.reason = reason;
    //     this.amount = amount;
    //     this.description = description;
    //     this.status = statusId;
    //     this.employeeID = employeeID;
    //     this.guid = "";
    // }

    public ReimburseTicket() {}
}
