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
        Employee PostEmployee(string email, string password);
        Employee PostEmployee(string email, string password, int roleId);
        Employee GetEmployee(string email);
        Employee GetEmployee(int id);
        Employee LoginEmployee(string email, string password);
    }

    public class EmployeeRepository : IEmployeeRepository {
        // Giving this class a logger
        private readonly ILoggerEmployeeRepository _loggerER;
        public EmployeeRepository(ILoggerEmployeeRepository logger) => this._loggerER = logger;

        #region // Put methods... update role, pass, or email
        public Employee UpdateEmployee(int id, int roleId) {
            string conString = File.ReadAllText("../../ConString.txt");
            using(SqlConnection connection = new SqlConnection(conString)) {
                string updateEmployeeQuery = "UPDATE Employee SET RoleId = @RoleId WHERE EmployeeId = @Id;";
                SqlCommand command = new SqlCommand(updateEmployeeQuery, connection);
                command.Parameters.AddWithValue("@RoleId", roleId);
                command.Parameters.AddWithValue("@Id", id); 

                try {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if(rowsAffected == 1) {
                        _loggerER.LogEmployeePut(true, roleId);
                        return GetEmployee(id);
                    } else {    
                        _loggerER.LogEmployeePut(false, roleId);
                        return null!;
                    } 
                } catch(Exception e) {
                    _loggerER.LogEmployeePut(false, roleId);
                    Console.WriteLine(e.Message);
                    return null!;
                }
            } 
        }

        public Employee UpdateEmployee(int id, string info) {
            string conString = File.ReadAllText("../../ConString.txt");
            string regex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            using(SqlConnection connection = new SqlConnection(conString)) {
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

                try {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if(rowsAffected == 1) {
                        _loggerER.LogEmployeePut(true, info);
                        return GetEmployee(id);
                    } else {
                        _loggerER.LogEmployeePut(false, info);
                        return null!;
                    }
                } catch(Exception e) {
                    _loggerER.LogEmployeePut(false, info);
                    Console.WriteLine(e.Message);
                    return null!;
                }
            }
        }
        #endregion

        #region // Post methods... create an employee with or without role
        public Employee PostEmployee(string email, string password) {
            string conStirng = File.ReadAllText("../../ConString.txt");
            using(SqlConnection connection = new SqlConnection(conStirng)) {
                string insertEmployeeQuery = "INSERT INTO Employee (Email, Password, RoleId) VALUES (@email, @password, @RoleId);";
                SqlCommand command = new SqlCommand(insertEmployeeQuery, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@RoleId", 0);
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

        public Employee PostEmployee(string email, string password, int roleId) {
            string conStirng = File.ReadAllText("../../ConString.txt");
            using(SqlConnection connection = new SqlConnection(conStirng)) {
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
            string conString = File.ReadAllText("../../ConString.txt");
            using(SqlConnection connection = new SqlConnection(conString)) {
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
            string conString = File.ReadAllText("../../ConString.txt");
            using(SqlConnection connection = new SqlConnection(conString)) {
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
            string conString = File.ReadAllText("../../ConString.txt");
            using(SqlConnection connection = new SqlConnection(conString)) {
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
    }
}