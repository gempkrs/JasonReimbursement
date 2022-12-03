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

using ModelLayer;
using BusinessLayer;

namespace Tests.Business
{
    public class XUnitValidationService
    {
        [Theory]
        [InlineData("testNormal@email.com")] // False
        [InlineData("UniqueEmail@email.com")] // True
        public void ValidEmail(string email) {
            IValidationService _ivs = new ValidationService();
            List<Employee> db = new List<Employee>();
            Employee existingEmployee = new Employee(0, "testNormal@email.com", "123Pass");
            db.Add(existingEmployee);
            if(existingEmployee.email.Equals(email))
                Assert.False(_ivs.ValidEmail(email, db));
            else
                Assert.True(_ivs.ValidEmail(email, db));
        }
    }
}