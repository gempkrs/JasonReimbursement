/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Importing necessary layers
using ModelLayer;

namespace BusinessLayer
{
    public interface IValidationService {
        public bool ValidEmail(string email, List<Employee> tmpDb);
    }

    public class ValidationService : IValidationService {
        public bool ValidEmail(string email, List<Employee> tmpDb) {
            return false;
        }
    }
}