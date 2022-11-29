using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Import our model
using ModelLayer;
namespace BusinessLayer;

public interface IEmployeeService {
    public bool RegisterEmployee(string email, string password);
}

public class EmployeeService : IEmployeeService {
    public bool RegisterEmployee(string email, string password) {
        return false;
    }
}