/* JASON TEJADA    PROJECT 1 MODEL LAYER CLASS    REVATURE
 * Desc:
 *        This class defines the Employee object for the Employee Reimbursement
 *        System. Datafields are: ID, role, email, and password. Service class
 *        for procedures will be defined in the business layer.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer;

public class Employee {
    // Primary key
    public int id;
    public string email;
    public string password;

    // 0 is default. 1 is manager.
    public int roleID;

    // Constructor for a normal employee
    public Employee(int id, string email, string password) {
        this.id = id;
        this.email = email;
        this.password = password;
        this.roleID = 0;
    }

    // Constructor intended for an employee with the manager role
    public Employee(int id, string email, string password, int roleID) {
        this.id = id;
        this.email = email;
        this.password = password;
        this.roleID = roleID;
    }
}
