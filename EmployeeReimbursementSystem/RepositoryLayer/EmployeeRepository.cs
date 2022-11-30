/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

// Importing necessary layers
using ModelLayer;

namespace RepositoryLayer
{
    public interface IEmployeeRepository {
        List<Employee> GetEmployees();
        //void PostEmployees(List<Employee> employeeDB);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetEmployees() {
            if(File.Exists("EmployeeDatabase.json")) {
                return JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText("EmployeeDatabase.json"))!;
            } else {
                return new List<Employee>();
            }
        }
    }
}