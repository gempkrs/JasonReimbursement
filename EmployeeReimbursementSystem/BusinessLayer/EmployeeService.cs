/* JASON TEJADA    PROJECT 1 BUSINESS LAYER CLASS    REVATURE 
 * Desc:
 *          This class defines the business logic for our employee
 *          requirements for the ERS.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Import our model
using ModelLayer;
// using RepositoryLayer;

namespace BusinessLayer;

// Interface to be used for dependency injection in the api layer
public interface IEmployeeService {
    public Employee RegisterEmployee(string email, string password);
}

public class EmployeeService : IEmployeeService {
    public Employee RegisterEmployee(string email, string password) {
        // TODO
        // Need to get a list of employees from the repository layer
        // Until then, use this list. When we use SQL, simply check
        // and then do an insert query. No need to get list.
        List<Employee> dbEmployee = new List<Employee>(); // TMP
        int id = dbEmployee.Count() + 1; // TMP: query count of db 
        //bool result = true;

        Employee newEmployee = new Employee(id, email, password);        
        // Validation
        // if(email.Length == 0 || password.Length == 0) result = false;
        // foreach(Employee entry in dbEmployee) {
        //     if((entry.email).Equals(email))
        //         result = false;
        // }

        //dbEmployee.Add(new Employee(id, email, password));
        // PostToDatabase(dbEmployee)... add list, later this will be
        // an insert query to insert an employee object
        //return result;
        return newEmployee;
    }
}