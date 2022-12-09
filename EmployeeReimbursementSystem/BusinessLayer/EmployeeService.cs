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
    public Employee PostEmployee(string email, string password);
    public Employee PostEmployee(string email, string password, int roleid);
    public Employee LoginEmployee(string email, string password);
    
    public Employee EditEmployee(int id, string oldPassword, string newPassword);
    public Employee EditEmployee(int id, string email);
    public Employee EditEmployee(int managerId, int employeeId, int roleId);
}

public class EmployeeService : IEmployeeService {

    private readonly IEmployeeRepository _ier;
    private IEmployeeValidationService _ievs;
    public EmployeeService(IEmployeeRepository ier) { 
        this._ier = ier;
        this._ievs = new EmployeeValidationService(_ier);
    }

    public Employee LoginEmployee(string email, string password) => _ier.LoginEmployee(email, password);

    #region //Registration methods
    public Employee PostEmployee(string email, string password) { 
        if(!_ievs.ValidRegistration(email, password)) 
            return null!;
        return _ier.PostEmployee(email, password);
    }

    // Overloaded method for registering a manager/employee with a role
    public Employee PostEmployee(string email, string password, int roleid) {
        if(!_ievs.ValidRegistration(email, password, roleid)) 
            return null!;
        return _ier.PostEmployee(email, password, roleid);
    }
    #endregion

    #region // TODO, Refactor: Edit Employee Methods
    public Employee EditEmployee(int id, string oldPassword, string newPassword) {
        // TODO, TMP; Do this until sql... in database, check if id exists, if it does update employee
        if(!_ievs.isEmployee(id) || !_ievs.ValidPassword(newPassword)) return null!;

        //tmp... update query using employee id...
        List<Employee> employeeDb = _ier.GetEmployees();
        foreach(Employee entry in employeeDb) {
            if(entry.id == id && entry.password.Equals(oldPassword)) {
                entry.password = newPassword;
                _ier.PostEmployees(employeeDb);
                return entry;
            }
        }
        return null!;
    }

    public Employee EditEmployee(int id, string email) {
        // TODO, TMP; Do this until sql... in database, check if id exists, if it does update employee
        if(!_ievs.isEmployee(id) || !_ievs.ValidEmail(email)) return null!;

        //tmp... update query using employee id...
        List<Employee> employeeDb = _ier.GetEmployees();
        foreach(Employee entry in employeeDb) {
            if(entry.id == id) {
                entry.email = email;
                _ier.PostEmployees(employeeDb);
                return entry;
            }
        }
        return null!;
    }
    public Employee EditEmployee(int managerId, int employeeId, int roleId) {
        // TODO, TMP; Do this until sql... in database, check if id exists, if it does update employee
        if(managerId == employeeId) return null!;

        if(!_ievs.isManager(managerId) || !_ievs.ValidRole(roleId) || !_ievs.isEmployee(employeeId)){
            Console.WriteLine("Manager exists, role invalid, or employee does not exist.");
            return null!;
        }
        return _ier.UpdateEmployee(employeeId, roleId);

        // //tmp... update query using employee id...
        // List<Employee> employeeDb = _ier.GetEmployees();
        // foreach(Employee entry in employeeDb) {
        //     if(entry.id == employeeId) {
        //         entry.roleID = roleId;
        //         _ier.PostEmployees(employeeDb);
        //         return entry;
        //     }
        // }
        //return null!;
    }
    #endregion
}