/* JASON TEJADA    PROJECT 1 BUSINESS LAYER CLASS    REVATURE 
 * Desc:
 *          This class defines the business logic for our employee
 *          requirements for the ERS. Implements main functionality
 *          for employees.
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
    private IEmployeeValidationService _ievs;
    public EmployeeService(IEmployeeRepository ier) { 
        this._ier = ier;
        this._ievs = new EmployeeValidationService(_ier);
    }

    public Employee RegisterEmployee(string email, string password) {
        // Once we use sql, will only need to do insert query in repo
        List<Employee> dbEmployee = _ier.GetEmployees(); 
        int id = dbEmployee.Count() + 1; //query count of db 

        if(!_ievs.ValidRegistration(email, password)) 
            return null!;

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

        if(!_ievs.ValidRegistration(email, password, roleid)) 
            return null!;

        Employee newEmployee = new Employee(id, email, password, roleid);
        
        dbEmployee.Add(newEmployee);
        _ier.PostEmployees(dbEmployee); 

        return newEmployee;
    }

    public Employee LoginEmployee(string email, string password) {
        List<Employee> dbEmployees = _ier.GetEmployees(); 

        // TODO Validation... validates itself, if we get no records return null from repo layer
        foreach(Employee entry in dbEmployees) {
            if((entry.email).Equals(email) && (entry.password).Equals(password)) {
                return entry;
            }
        }
        
        return null!;
    }
}