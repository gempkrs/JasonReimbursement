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
    // TODO Refactor to work with new system
    /* CONSTRUCTOR TEST, ADDING EMPLOYEE TO EMPTY DATABASE
     * Tests if we successfully added an employee object to an empty list of 
     * employees. Simulates expected input from the user and expected logic from
     * the business layer.
     * Passes if an employee to be added to the list is constructed with ID of 1.
     */
    // [Theory]
    // [InlineData("test@real.com", "WeakPass123")] // Pass
    // public void CreateEmployeeFromEmptyList(string email, string password) {
    //     // Arrange
    //     /* LOGIC FROM BUSINESS LAYER
    //      * In the system, repo layer accesses a list of our employees. If there
    //      * is no database, create it; i.e. return an empty list of employees to 
    //      * the business layer to be saved after we add our first entry.
    //      */
    //     List<Employee> dbEmployee = new List<Employee>();
    //     int employeeID = dbEmployee.Count() + 1;

    //     // Act
    //     Employee newEmployee = new Employee(employeeID, email, password);
    //     dbEmployee.Add(newEmployee);

    //     // Assert
    //     Assert.Equal(1, dbEmployee[0].id);
    //     Assert.Equal(0, dbEmployee[0].roleID);
    //     Assert.Equal(email, dbEmployee[0].email);
    //     Assert.Equal(password, dbEmployee[0].password);    
    // }

    // /*
    //  * Essentially the same tests as CreateEmployeeFromEmptyList, but we call a different constructor for making an employee with manager permissions.
    //  * 0 is the employee role, 1 is the manager role.
    //  */
    // [Theory]
    // [InlineData("testmanager@real.com", "ManPass123", 1)] // Pass
    // [InlineData("testmanager@real.com", "ManPass123", 0)] // Pass
    // [InlineData("testmanager@real.com", "ManPass123", 2)] // Fail; not 1 or 0
    // [InlineData("testmanager@real.com", "ManPass123", -1)] // Fail
    // public void CreateEmployeeWithPermsFromEmptyList(string email, string password, int roleID) {
    //     //Arrange
    //     List<Employee> dbEmployee = new List<Employee>();
    //     bool validRole = (roleID != 0 || roleID != 1);
    //     int employeeID = dbEmployee.Count() + 1;

    //     //Act
    //     if(!validRole) { Assert.False(validRole); }
    //     Employee newEmployee = new Employee(employeeID, email, password, roleID);
    //     dbEmployee.Add(newEmployee);

    //     //Assert
    //     Assert.Equal(1, dbEmployee[0].id);
    //     Assert.Equal(roleID, dbEmployee[0].roleID);
    //     Assert.Equal(email, dbEmployee[0].email);
    //     Assert.Equal(password, dbEmployee[0].password);
    //}
}