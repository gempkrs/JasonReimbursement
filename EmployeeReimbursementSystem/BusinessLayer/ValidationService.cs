/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Importing necessary layers
using ModelLayer;
using RepositoryLayer;

// Library used in email validation... Easier than using a regex
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public interface IEmployeeValidationService {
        public bool ValidEmail(string email);
        public bool ValidPassword(string pass);
        public bool ValidRole(int roleId);
        public bool ValidRegistration(string email, string pass);
        public bool ValidRegistration(string email, string pass, int roleId);
        
    }

    public class EmployeeValidationService : IEmployeeValidationService {
        private readonly IEmployeeRepository _ier;
        public EmployeeValidationService(IEmployeeRepository ier) => this._ier = ier;

        public bool ValidRegistration(string email, string pass) => ValidEmail(email) && ValidPassword(pass);
        public bool ValidRegistration(string email, string pass, int roleId) => ValidEmail(email) && ValidPassword(pass) && ValidRole(roleId);

        #region // Functions for validating registration
        // Validate input format and ensure the email exists.
        public bool ValidEmail(string email) {
            string regex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            bool validInput = Regex.Match(email, regex).Success;
            // Should start as false, if query returns no records we have unique email.
            bool uniqueEmail = true;
            
            // TMP
            foreach(Employee e in _ier.GetEmployees()) {
                if(e.email.Equals(email)) uniqueEmail = false;
            }

            return (validInput && uniqueEmail);
        }
        public bool ValidPassword(string pass) => Regex.Match(pass, @"^([0-9a-zA-Z]{6,})$").Success;
        public bool ValidRole(int roleId) => (roleId >= 0 && roleId <= 1);
        #endregion
    
    }
}