/* JASON TEJADA    PROJECT 1 TESTING LAYER FOR BUSINESS    REVATURE
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Business;
public class XUnitEmployeeService {
    [Theory]
    [InlineData("passtest@email.com", "123Pass")] // Pass
    [InlineData("failtest@email.com", "123Fail")] // Fail
    [InlineData("failtest@email.com", "")] // Fail
    [InlineData("", "123Fail")] // Fail
    [InlineData("", "")] // Fail
    public void EmployeePostToDatabase(string email, string password) {
        // Arrange
        // Act
        // Assert
    }
}