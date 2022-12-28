using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ILoggerEmployeeRepository {
        // Logger for GET requests
        void LogEmployeeGet(bool success, object o);

        // Logger for POST requests
        void LogEmployeePost(bool success);

        // Logger for PUT requests
        void LogEmployeePut(bool success, object o);
        void LogLoginRequest(bool success);
    }

    public class LoggerEmployeeRepository : ILoggerEmployeeRepository {
        public void LogEmployeeGet(bool success, object o) {
            if(success)
                Console.WriteLine($"GET request for Employee was successful using {o} argument.");
            else
                Console.WriteLine($"GET request for Employee FAILED using {o} argument");
        }

        public void LogEmployeePost(bool success) {
            if(success)
                Console.WriteLine($"POST request for Employee was successful with passed arguments.");
            else
                Console.WriteLine($"POST request for Employee FAILED using passed arguments");
        }

        public void LogEmployeePut(bool success, object o) {
            if(success)
                Console.WriteLine($"PUT request for Employee was successful using {o} argument.");
            else
                Console.WriteLine($"PUT request for Employee FAILED using {o} argument");
        }

        public void LogLoginRequest(bool success) {
            if(success) {
                Console.WriteLine($"Login request successful");
            }
            else
                Console.WriteLine($"Login request FAILED");
        }
    }
}