using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

using ModelLayer;

/**
 * TODO, Reduce the number of repeated lines of code and increase readability
 * - Identify where code keeps getting reused, seperate functionality into 
 *   seperate methods.
 * - Add in loggers.
 */
namespace RepositoryLayer
{
    public interface IEmployeeRepository {
        Employee UpdateEmployee(int id, int roleId);
        Employee UpdateEmployee(int id, string info);
        Employee PostEmployee(string email, string password, int roleId);
        Employee GetEmployee(string email);
        Employee GetEmployee(int id);
        Employee LoginEmployee(string email, string password);
    }

    public class EmployeeRepository : IEmployeeRepository {
        // Giving this class a logger
        private readonly ILoggerEmployeeRepository _loggerER;
        private string _conString;
        public EmployeeRepository(ILoggerEmployeeRepository logger) {
            this._loggerER = logger;
            this._conString = File.ReadAllText("../../ConString.txt");
        } 

        #region // Put methods... update role, pass, or email
        public Employee UpdateEmployee(int id, int roleId) {
            using(SqlConnection connection = new SqlConnection(_conString)) {
                string updateEmployeeQuery = "UPDATE Employee SET RoleId = @RoleId WHERE EmployeeId = @Id;";
                SqlCommand command = new SqlCommand(updateEmployeeQuery, connection);
                command.Parameters.AddWithValue("@RoleId", roleId);
                command.Parameters.AddWithValue("@Id", id); 

                return ExecuteUpdate(connection, command, id, "UpdateRole");
            } 
        }

        public Employee UpdateEmployee(int id, string info) {
            string regex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            using(SqlConnection connection = new SqlConnection(_conString)) {
                string updateEmployeeQuery;
                if(System.Text.RegularExpressions.Regex.Match(info, regex).Success) {
                    // info is an email
                    updateEmployeeQuery = "UPDATE Employee SET Email = @info WHERE EmployeeId = @Id";
                } else {
                    // info is a password
                    updateEmployeeQuery = "UPDATE Employee SET Password = @info WHERE EmployeeId = @Id";
                }
                SqlCommand command = new SqlCommand(updateEmployeeQuery, connection);
                command.Parameters.AddWithValue("@info", info); 
                command.Parameters.AddWithValue("@Id", id);

                return ExecuteUpdate(connection, command, id, info);
            }
        }
        #endregion

        #region // Post method... create an employee/manager
        public Employee PostEmployee(string email, string password, int roleId) {
            using(SqlConnection connection = new SqlConnection(_conString)) {
                string insertEmployeeQuery = "INSERT INTO Employee (Email, Password, RoleId) VALUES (@email, @password, @RoleId);";
                SqlCommand command = new SqlCommand(insertEmployeeQuery, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@RoleId", roleId);
                try {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if(rowsAffected == 1) {
                        _loggerER.LogEmployeePost(true);
                        return GetEmployee(email);
                    } else {
                        _loggerER.LogEmployeePost(false);
                        return null!;
                    }
                } catch (Exception e) {
                    _loggerER.LogEmployeePost(false);
                    Console.WriteLine(e.Message);
                    return null!;
                }
            }
        }
        #endregion

        #region  // Get Methods... retrieve unique employee by email, id, or email & password
        public Employee GetEmployee(string email) {
            using(SqlConnection connection = new SqlConnection(_conString)) {
                string queryEmployeeByEmail = "SELECT * FROM Employee WHERE Email = @email";
                SqlCommand command = new SqlCommand(queryEmployeeByEmail, connection);
                command.Parameters.AddWithValue("@Email", email);
                try {
                    connection.Open();
                    
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        if(!reader.HasRows) {
                            _loggerER.LogEmployeeGet(false, email);
                            return null!;
                        } 
                        else {
                            reader.Read();
                            _loggerER.LogEmployeeGet(true, email);
                            return new Employee(
                                (int)reader[0], 
                                (string)reader[1], 
                                (string)reader[2], 
                                (int)reader[3]
                            );
                        }
                    }
                } catch(Exception e) {
                    _loggerER.LogEmployeeGet(false, email);
                    Console.WriteLine(e.Message);
                    return null!;
                }
            }
        }

        public Employee GetEmployee(int id) {
            using(SqlConnection connection = new SqlConnection(_conString)) {
                string queryEmployeeById = "SELECT * FROM Employee WHERE EmployeeId = @id";
                SqlCommand command = new SqlCommand(queryEmployeeById, connection);
                command.Parameters.AddWithValue("@id", id);
                try {
                    connection.Open();
                    
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        if(!reader.HasRows) {
                            _loggerER.LogEmployeeGet(false, id);
                            return null!;
                        } 
                        else {
                            reader.Read();
                            _loggerER.LogEmployeeGet(true, id);
                            return new Employee(
                                (int)reader[0], 
                                (string)reader[1], 
                                (string)reader[2], 
                                (int)reader[3]
                            );
                        }
                    }
                } catch(Exception e) {
                    _loggerER.LogEmployeeGet(false, id);
                    Console.WriteLine(e.Message);
                    return null!;
                }
            }
        }

        public Employee LoginEmployee(string email, string password) {
            using(SqlConnection connection = new SqlConnection(_conString)) {
                string queryEmployeeByEmail = "SELECT * FROM Employee WHERE Email = @email AND Password = @password";
                SqlCommand command = new SqlCommand(queryEmployeeByEmail, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                try {
                    connection.Open();
                    
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        if(!reader.HasRows) {
                            _loggerER.LogLoginRequest(false);
                            return null!;
                        }
                        else {
                            reader.Read();
                            _loggerER.LogLoginRequest(true);
                            return new Employee(
                                (int)reader[0], 
                                (string)reader[1], 
                                (string)reader[2], 
                                (int)reader[3]
                            );
                        }
                    }
                } catch(Exception e) {
                    _loggerER.LogLoginRequest(false);
                    Console.WriteLine(e.Message);
                    return null!;
                }
            }
        }
        #endregion

        private Employee ExecuteUpdate(SqlConnection con, SqlCommand comm, int id, object logInfo) {
            // Steps for updating
            try { 
                con.Open();
                int rowsAffected = comm.ExecuteNonQuery();
                if(rowsAffected == 1) {
                    _loggerER.LogEmployeePut(true, logInfo);
                    return GetEmployee(id);
                } else {    
                    _loggerER.LogEmployeePut(false, logInfo);
                    return null!;
                } 
            } catch(Exception e) {
                _loggerER.LogEmployeePut(false, logInfo);
                Console.WriteLine(e.Message);
                return null!;
            }
        }
    }
}