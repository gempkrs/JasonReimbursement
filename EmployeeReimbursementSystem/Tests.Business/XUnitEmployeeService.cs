/* JASON TEJADA    PROJECT 1 TESTING LAYER FOR BUSINESS    REVATURE
 * Desc:
 *          This program tests our employee service class. Tests if the
 *          employee service class does what we want. More specifically, test
 *          if we can successfully implement our requirements for the business 
 *          layer: RegisterEmployee
 *          
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// Import necessary layers
using ModelLayer;
using BusinessLayer;
// using RepositoryLayer;

namespace Tests.Business;
public class XUnitEmployeeService {
    [Theory]
    [InlineData("passtest@email.com", "123Pass")] // Pass
    [InlineData("failtest@email.com", "123Fail")] // Fail
    [InlineData("failtest2@email.com", "")] // Fail
    [InlineData("", "123Fail")] // Fail
    [InlineData("", "")] // Fail

 
    /* TODO: complete the test, first need to complete the database...
     * Until then, we will fail the assertion on line 49 for test 2
     */
    public void RegisterValidEmployeeToDatabase(string email, string password) {
        // Arrange
        // List<Employee> dbEmployee = new List<Employee>();
        // dbEmployee.Add(new Employee(1, "failtest@email.com", "123"));
        // EmployeeService empServ = new EmployeeService();

        // Act
        // bool employeeRegistered = empServ.RegisterEmployee(email, password);

        // Assert
        // Make sure user doesn't register with empty strings
        //if(email.Length == 0 || password.Length == 0) 
            // Assert.False(employeeRegistered);
        
        // Make sure the user is using a unique email
        // foreach(Employee e in dbEmployee) {
        //     if((e.email).Equals(email)) 
        //         Assert.False(employeeRegistered);
        // }
    }
}