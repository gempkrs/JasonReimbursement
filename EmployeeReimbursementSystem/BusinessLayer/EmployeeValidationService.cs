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

// Regex library to enforce password and email constraints
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public interface IEmployeeValidationService {
        public bool ValidEmail(string email);
        public bool ValidPassword(string pass);
        public bool ValidRole(int roleId);
        public bool ValidRegistration(string email, string pass);
        public bool ValidRegistration(string email, string pass, int roleId);
        // TODO?, IMPLEMENT: public bool EmailExists(string email);
        public bool isEmployee(int id);
        public bool isManager(int id);
    }

    public class EmployeeValidationService : IEmployeeValidationService {
        private readonly IEmployeeRepository _ier;
        public EmployeeValidationService(IEmployeeRepository ier) => this._ier = ier;

        public bool ValidRegistration(string email, string pass) => ValidEmail(email) && ValidPassword(pass);
        public bool ValidRegistration(string email, string pass, int roleId) => ValidEmail(email) && ValidPassword(pass) && ValidRole(roleId);

        #region // Functions for validating registration
        // Validate input format and ensure the email exists.
        /*
            Using the unique constraint for the email column gives us an error when we try to insert
            with the same email. In the repo layer, catch this error and return null. Here, simply make
            sure the user doesn't enter a trash email.
            Using the foreign key constraint for the RoleId column gives us an error when we try to insert
            using an invalid role id. We do not need the ValidRole function anymore.
            May not need the isEmployee or the isManager function anymore...
        */
        public bool ValidEmail(string email) {
            string regex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            return Regex.Match(email, regex).Success;
        }            
        public bool ValidPassword(string pass) => Regex.Match(pass, @"^([0-9a-zA-Z]{6,})$").Success;
        public bool ValidRole(int roleId) => (roleId >= 0 && roleId <= 1);
        #endregion

        public bool isEmployee(int id) {
            // TODO Change to use SQL, if the query returns 0 records, employee doesn't exist            
            if(_ier.GetEmployee(id) is null) return false;
            else return true;
        }

        public bool isManager(int id) {
            // TODO Change to use sql, if the query returns 0 records, employee doesn't exist or has no permissions
            Employee tmp = _ier.GetEmployee(id);
            if(tmp is null || tmp.roleID == 0) return false;
            else return true;
        }
    }
}