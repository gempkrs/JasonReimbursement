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
using RepositoryLayer;

namespace Tests.Business;
public class XUnitEmployeeService {
    [Theory]
    [InlineData("passtest@email.com", "123Pass")]
    [InlineData("test@email.com", "123Fail")]
    [InlineData("failtest2@email.com", "")]
    [InlineData("", "123Fail")]
    [InlineData("", "")]
    public void RegisterValidEmployeeToDatabase(string email, string password) {
        // Arrange
        IEmployeeRepository ier = new EmployeeRepository();
        List<Employee> dbEmployee = ier.GetEmployees();
        dbEmployee.Add(new Employee(1, "test@email.com", "123456"));
        IEmployeeService _ies = new EmployeeService(ier);

        // Act
        Employee newEmployee = _ies.RegisterEmployee(email, password);

        // Assert
        //Make sure user registers with our conditions
        if(email.Length < 1 || password.Length < 1) 
            Assert.True(newEmployee is null);
        
        // Make sure the user is using a unique email
        foreach(Employee e in dbEmployee) {
            if((e.email).Equals(email)) 
                Assert.True(newEmployee is null);
        }
    }

    [Theory]
    // Can't pass test for valid email, even though LoginEmployee works as expected...
    [InlineData("test@email.com", "123Pass")] 
    [InlineData("test@email.com", "123Fail")]
    [InlineData("FailTest@email.com", "123Pass")]
    [InlineData("", "")]
    [InlineData("", "FailTest")]
    [InlineData("FailTest@email.com", "")]
    public void LoginEmployeeWithValidCredentials(string email, string password) {
        // Arrange
        IEmployeeRepository ier = new EmployeeRepository();
        List<Employee> db = ier.GetEmployees();
        IEmployeeService _ies = new EmployeeService(ier);
        //List<Employee> db = ier.GetEmployees();
        db.Add(new Employee(1, "test@email.com", "123Pass"));

        //Act
        Employee validEmployee = _ies.LoginEmployee(email, password);
        //Employee validEmployee = new Employee(db.Count()+1, email, password);
        bool valid = false;

        //Assert
        foreach(Employee entry in db) {
            if((entry.email).Equals(email) && (entry.password).Equals(password)) {
                valid = true;
            }
        }

        if(!valid) {
            Assert.True(validEmployee is null);
        } else {
            Assert.True(validEmployee is not null);
        }
    }
}