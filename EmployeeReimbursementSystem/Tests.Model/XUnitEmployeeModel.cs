/* JASON TEJADA    PROJECT 1 TESTING LAYER FOR MODEL    REVATURE
 * Desc:
 *        This class tests our employee model. More specfically, tests if an
 *        employee object is instantiated correctly under certain conditions. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// Importing our employee
using ModelLayer;

namespace Tests.Model;
public class XUnitEmployeeModel {
    
    /* CONSTRUCTOR TEST, ADDING EMPLOYEE TO EMPTY DATABASE
     * Tests if we successfully added an employee object to an empty list of 
     * employees. Simulates expected input from the user and expected logic from
     * the business layer.
     * Passes if an employee to be added to the list is constructed with ID of 1.
     */
    [Fact]
    public void CreateEmployeeFromEmptyList() {
        // Arrange
        
        /* LOGIC FROM BUSINESS LAYER
         * In the system, repo layer accesses a list of our employees. If there
         * is no database, create it; i.e. return an empty list of employees to 
         * the business layer to be saved after we add our first entry.
         */
        List<Employee> dbEmployee = new List<Employee>();
        
        // Expected user input
        string email = "test@real.com";
        string password = "WeakPass123";

        // Act
        int employeeID = dbEmployee.Count() + 1;
        Employee newEmployee = new Employee(employeeID, email, password);

        // Assert
        Assert.Equal(1, newEmployee.id);
        Assert.Equal(0, newEmployee.role);
        Assert.Equal(email, newEmployee.email);
        Assert.Equal(password, newEmployee.password);    
    }
}