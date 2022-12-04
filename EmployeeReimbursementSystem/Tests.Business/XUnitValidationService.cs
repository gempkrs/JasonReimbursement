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
        [InlineData("test@email.com")] // False, exists
        public void ValidateEmail(string email) {
            // Arrange
            IValidationService _ivs = new ValidationService();
            
            // Act
            bool validEmail = _ivs.ValidEmail(email);
            bool check = MailAddress.TryCreate(email, out MailAddress ?result);
            
            // Assert
            if(check == true)
                Assert.True(validEmail);
            else
                Assert.False(validEmail);
            
            /* THIS PART WILL MORE OR LESS BE TAKEN CARE OF BY OUR REPO LAYER
            List<Employee> db = new List<Employee>();
            Employee existingEmployee = new Employee(0, "testNormal@email.com", "123Pass");
            db.Add(existingEmployee);
            foreach(Employee e in db) {
                if(e.email.Equals(email))
                    Assert.False(_ivs.ValidEmail(email, db));
                else
                    Assert.True(_ivs.ValidEmail(email, db));
            }
            */
        }
    }
}