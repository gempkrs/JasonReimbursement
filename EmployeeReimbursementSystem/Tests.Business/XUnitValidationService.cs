/* JASON TEJADA    PROJECT 1 TESTING LAYER FOR BUSINESS    REVATURE
 * Desc:
 *          Unit tests for ValidationService class. Tests if the validation
 *          class works as expected. More specifically, tests if the various
 *          methods in the ValidateService class correctly validates employee 
 *          & ticket operations.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using BusinessLayer;
using RepositoryLayer;
using ModelLayer;
using System.Net.Mail;

namespace Tests.Business
{
    public class XUnitValidationService
    {
        [Theory]
        [InlineData("pass@email.com")] // True
        [InlineData("UniqueEmail@email.com")] // True
        [InlineData("testemail.com")]  // False, wrong format
        [InlineData("test")]           // False, wrong format
        [InlineData("newTestEmail@email")] // False, wrong format
        public void ValidateEmailFormat(string email) {
            // Arrange
            IValidationService _ivs = new ValidationService(new EmployeeRepository());
            
            // Act
            bool validEmail = _ivs.ValidEmail(email);
            bool check = MailAddress.TryCreate(email, out MailAddress ?result);
            
            // Assert
            if(check == true)
                Assert.True(validEmail);
            else
                Assert.False(validEmail);
        }

        [Theory]
        [InlineData("test@email.com")] // Exists
        [InlineData("DoesntExist@email.com")] // Unique
        public void UnqiueEmailValidation(string email) {
            // Arrange
            IValidationService _ivs = new ValidationService(new EmployeeRepository());
            bool emailExists = false;
            List<string> existingTestEmails = new List<string> {
                "test@email.com",
            };

            // Act
            foreach(string entry in existingTestEmails) {
                if(entry.Equals(email)) emailExists = true;
            }

            // Assert, Email already exists
            if(emailExists) Assert.False(_ivs.ValidEmail(email));
        }

        [Theory]
        [InlineData(0)] // True
        [InlineData(1)] // True
        [InlineData(2)] // False
        [InlineData(-1)] // False
        public void RoleValidation(int roleId) {
            // Arrange
            IValidationService _ivs = new ValidationService(new EmployeeRepository());

            // Assert
            if(_ivs.ValidRole(roleId)) 
                Assert.True(roleId == 0 || roleId == 1);
            else Assert.False(_ivs.ValidRole(roleId));
        }
    }
}