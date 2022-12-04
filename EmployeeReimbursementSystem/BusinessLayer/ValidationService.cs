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
using System.Net.Mail;

namespace BusinessLayer
{
    public interface IValidationService {
        public bool ValidEmail(string email);
    }

    public class ValidationService : IValidationService {
        private readonly IEmployeeRepository _ier;
        public ValidationService(IEmployeeRepository ier) => this._ier = ier;

        // Validate input format and ensure the email exists.
        public bool ValidEmail(string email) {
            bool validInput = MailAddress.TryCreate(email, out MailAddress ?result);
            bool uniqueEmail = true;
            /* IF QUERY RETURNS NO RECORDS, WE HAVE A UNIQUE EMAIL */
            
            // TMP
            foreach(Employee e in _ier.GetEmployees()) {
                if(e.email.Equals(email)) uniqueEmail = false;
            }

            return (validInput && uniqueEmail);
        }
    }
}