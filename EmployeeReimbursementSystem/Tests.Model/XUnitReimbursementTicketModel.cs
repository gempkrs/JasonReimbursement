/* JASON TEJADA    PROJECT 1 TESTING LAYER FOR MODEL    REVATURE
 * Desc:
 *        This class tests our employee model. More specfically, tests if a
 *        reimbursement ticket object is instantiated correctly under certain 
 *        conditions. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// Importing our ticket
using ModelLayer;

namespace Tests.Model;
// TODO Refactor with new constructor
public class XUnitReimbursementTicketModel {
    
    /* CONSTRUCTOR TEST, ADDING TICKET TO EMPTY DATABASE
     * Tests if we successfully added a ticket object to an empty list of 
     * tickets. Simulates expected input from the user and expected logic from
     * the business layer.
     */
    // [Theory]
    // [InlineData(1, "Travel", 500, "I am trying to relocate.")] // Pass
    // [InlineData(0, "True", 1, "True")] // Fail
    // [InlineData(1, "", -1, "")] // Fail
    // [InlineData(1, "True", -1, "True")] // Fail
    // [InlineData(1, "True", 0, "True")] // Fail
    // [InlineData(1, "", 1, "True")] // Fail
    // [InlineData(1, "True", 1, "")] // Fail
    // public void CreateTicketFromEmptyList(int employeeID, string reason, int amount, string description){
    //     // Arrange
    //    /* LOGIC FROM BUSINESS LAYER
    //     * Like with employees, repo layer accesses a list of our tickets. If there
    //     * is no database, create it; return an empty list of tickets to the business
    //     * layer (to be saved after we add our first entry... later).
    //     * Since this ticket belongs to an employee, we will need to check if the
    //     * employee exists in the system. For testing purposes, we will have a test
    //     * employee database, fails in the case the user tries to add a ticket with
    //     * an invalid ID. In the program, repo layer also accesses a list of employees
    //     */
    //     // Setting up test database for employee to use in verification
    //     List<Employee> dbEmployee = new List<Employee>();
    //     dbEmployee.Add(new Employee(1, "test@real.com", "Pass123"));
    //     bool employeeCheck = false;

    //     // Check if the user entered the arguments correctly
    //     List<ReimburseTicket> dbTicket = new List<ReimburseTicket>();
    //     int ticketID = dbTicket.Count() + 1;
    //     if(reason.Length == 0 || description.Length == 0 || amount <= 0) { 
    //         Assert.False(false);
    //     }

    //     // Check if the employee id is valid
    //     foreach(Employee entry in dbEmployee) {
    //         if(employeeID == entry.id) { employeeCheck = true; }
    //     }
    //     if(!employeeCheck) { Assert.False(employeeCheck); }
        
    //     // Act
    //     ReimburseTicket newTicket = new ReimburseTicket(ticketID, employeeID, reason, amount, description);
    //     dbTicket.Add(newTicket);

    //     // Assert
    //     Assert.Equal(1, dbTicket[0].id);
    //     Assert.Equal(employeeID, dbTicket[0].employeeID);
    //     Assert.Equal(reason, dbTicket[0].reason);
    //     Assert.Equal(amount, dbTicket[0].amount);
    //     Assert.Equal(description, dbTicket[0].description);
    //     Assert.Equal(0, dbTicket[0].status);
//     }
// }
}