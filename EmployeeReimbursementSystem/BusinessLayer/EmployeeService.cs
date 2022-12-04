/* JASON TEJADA    PROJECT 1 BUSINESS LAYER CLASS    REVATURE 
 * Desc:
 *          This class defines the business logic for our employee
 *          requirements for the ERS.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Import our necessary layers
using ModelLayer;
using RepositoryLayer;

namespace BusinessLayer;

// Interface to be used for dependency injection in the api layer
public interface IEmployeeService {
    public Employee RegisterEmployee(string email, string password);
    public Employee RegisterEmployee(string email, string password, int roleid);
    public Employee LoginEmployee(string email, string password);
}

// TODO: Make user/ticket validation it's own class(es) & use it when validation is necessary
public class EmployeeService : IEmployeeService {

    private readonly IEmployeeRepository _ier;
    private IValidationService _ivs;
    public EmployeeService(IEmployeeRepository ier) { 
        this._ier = ier;
        this._ivs = new ValidationService(_ier);
    }

    public Employee RegisterEmployee(string email, string password) {
        // Once we use sql, will only need to do insert query in repo
        List<Employee> dbEmployee = _ier.GetEmployees(); 
        int id = dbEmployee.Count() + 1; //query count of db 

        // TODO Validation
        if(!_ivs.ValidEmail(email) || password.Length < 5) 
            return null!;
        // foreach(Employee entry in dbEmployee) {
        //     if((entry.email).Equals(email))
        //         return null!;
        // }

        // Create new employee object
        Employee newEmployee = new Employee(id, email, password);
        
        // later change this to an insert query to update db
        dbEmployee.Add(newEmployee);
        _ier.PostEmployees(dbEmployee); 
        
        return newEmployee;
    }

    // Overloaded method for registering a manager/employee with a role
    public Employee RegisterEmployee(string email, string password, int roleid) {
        List<Employee> dbEmployee = _ier.GetEmployees(); // TMP
        int id = dbEmployee.Count() + 1; // TMP

        // TODO Validation
        if(!_ivs.ValidEmail(email) || password.Length < 5 || (0 > roleid || roleid > 1)) 
            return null!;
        // foreach(Employee entry in dbEmployee) {
        //     if((entry.email).Equals(email))
        //         return null!;
        // }

        Employee newEmployee = new Employee(id, email, password, roleid);
        
        dbEmployee.Add(newEmployee);
        _ier.PostEmployees(dbEmployee); 

        return newEmployee;
    }

    public Employee LoginEmployee(string email, string password) {
        List<Employee> dbEmployees = _ier.GetEmployees(); 

        // TODO Validation
        foreach(Employee entry in dbEmployees) {
            if((entry.email).Equals(email) && (entry.password).Equals(password)) {
                return entry;
            }
        }
        
        return null!;
    }
}