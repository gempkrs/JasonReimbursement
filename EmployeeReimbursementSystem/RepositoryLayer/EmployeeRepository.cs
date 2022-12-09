/* JASON TEJADA    PROJECT 1 REPOSITORY LAYER CLASS    REVATURE
 * Desc:
 *          This class contains functions to interact with our database.
 *          GetEmployees returns a deserialized json employee database as a list
 *          of employees. PostEmployees serializes a list of employees into
 *          a json employee database. We will work with the list in the
 *          service class.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

// Importing necessary layers
using ModelLayer;

// TODO Refactor
namespace RepositoryLayer
{
    public interface IEmployeeRepository {
        List<Employee> GetEmployees();
        void PostEmployees(List<Employee> employeeDB);
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

        public void PostEmployees(List<Employee> employeeDb) {
            string serializedDb = JsonSerializer.Serialize(employeeDb);
            File.WriteAllText("EmployeeDatabase.json", serializedDb);
        }
    }
}