/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Importing necessary layers
using ModelLayer;

// Library used in email validation... Easier than using a regex
using System.Net.Mail;

namespace BusinessLayer
{
    public interface IValidationService {
        public bool ValidEmail(string email);
    }

    public class ValidationService : IValidationService {

        // Validate input format and ensure the email exists.
        public bool ValidEmail(string email) {
            bool inputCheck = MailAddress.TryCreate(email, out MailAddress ?result);
            bool emailCheck = false;
            return (inputCheck && emailCheck);
        }
    }
}