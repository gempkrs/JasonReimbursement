/*
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Data.SqlClient;

// Importing necessary layers
using ModelLayer;


namespace RepositoryLayer;

public interface ITicketRepository {
    ReimburseTicket PostTicket(string guid, string r, int a, string d, int eId);
    ReimburseTicket GetTicket(string ticketId);
    ReimburseTicket UpdateTicket(string ticketId, int statusId);
    List<ReimburseTicket> GetTickets(int employeeId);
    List<ReimburseTicket> GetTickets(int employeeId, int statusId);
    List<ReimburseTicket> GetPending(int managerId);
}

public class TicketRepository : ITicketRepository {
    public ReimburseTicket UpdateTicket(string ticketId, int statusId) {
        string conString = File.ReadAllText("../../ConString.txt");
        using(SqlConnection connection = new SqlConnection(conString)) {
            string updateTicketQuery = "UPDATE Ticket SET StatusId = @statusId WHERE TicketId = @ticketId";
            SqlCommand command = new SqlCommand(updateTicketQuery, connection);
            command.Parameters.AddWithValue("@statusId", statusId);
            command.Parameters.AddWithValue("@ticketId", ticketId);
            try {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if(rowsAffected == 1) {
                    Console.WriteLine("Update Success");
                    return GetTicket(ticketId);
                } else {
                    return null!;
                }
            } catch(Exception e) {
                Console.WriteLine("Update Failure\n" + e.Message);
                return null!;
            }
        }
    }

    public ReimburseTicket PostTicket(string guid, string r, int a, string d, int eId) {
        string conString = File.ReadAllText("../../ConString.txt");
        using(SqlConnection connection = new SqlConnection(conString)) {
            string insertTicketQuery = "INSERT INTO Ticket (TicketId, Reason, Amount, Description, StatusId, EmployeeId) VALUES (@guid, @r, @a, @d, 0, @eId);";
            SqlCommand command = new SqlCommand(insertTicketQuery, connection);
            command.Parameters.AddWithValue("@guid", guid);
            command.Parameters.AddWithValue("@r", r);
            command.Parameters.AddWithValue("@a", a);
            command.Parameters.AddWithValue("@d", d);
            command.Parameters.AddWithValue("@eId", eId);

            try {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if(rowsAffected == 1) {
                    Console.WriteLine("Post Success");
                    return GetTicket(guid);
                } else {
                    return null!;
                }
            } catch(Exception e) {
                Console.WriteLine("Insertion Failure\n" + e.Message);
                return null!;
            }
        }
    }

    public ReimburseTicket GetTicket(string ticketId) {
        string conString = File.ReadAllText("../../ConString.txt");
        using(SqlConnection connection = new SqlConnection(conString)) {
            string queryTicketById = "SELECT * FROM Ticket WHERE TicketId = @ticketId;";
            SqlCommand command = new SqlCommand(queryTicketById, connection);
            command.Parameters.AddWithValue("@ticketId", ticketId);
            try {
                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader()) {
                    if(!reader.HasRows) return null!;
                    else {
                        reader.Read();
                        return new ReimburseTicket(
                            (string) reader[0],
                            (string) reader[1],
                            (int) reader[2],
                            (string) reader[3],
                            (int) reader[4],
                            (int) reader[5]
                        );
                    }
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return null!;
            }
        }
    }

    public List<ReimburseTicket> GetTickets(int employeeId) {
        string conString = File.ReadAllText("../../ConString.txt");
        List<ReimburseTicket> employeeTickets = new List<ReimburseTicket>();
        using(SqlConnection connection = new SqlConnection(conString)) {
            string queryAllEmployeeTickets = "SELECT * FROM Ticket WHERE EmployeeId = @employeeId;";
            SqlCommand command = new SqlCommand(queryAllEmployeeTickets, connection);
            command.Parameters.AddWithValue("@employeeId", employeeId);

            try {
                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader()) {
                    if(!reader.HasRows) return null!;
                    while(reader.Read()) {
                        ReimburseTicket newTicket = new ReimburseTicket(
                            (string) reader[0],
                            (string) reader[1],
                            (int) reader[2],
                            (string) reader[3],
                            (int) reader[4],
                            (int) reader[5]
                        );
                        employeeTickets.Add(newTicket);
                    }
                    Console.WriteLine("GET Success");
                    return employeeTickets;
                }
            } catch(Exception e) {
                Console.WriteLine("GET error\n" + e.Message);
                return null!;
            }
        }
    }

    public List<ReimburseTicket> GetTickets(int employeeId, int statusId) {
        string conString = File.ReadAllText("../../ConString.txt");
        List<ReimburseTicket> employeeTickets = new List<ReimburseTicket>();
        using(SqlConnection connection = new SqlConnection(conString)) {
            string queryAllEmployeeTickets = "SELECT * FROM Ticket WHERE EmployeeId = @employeeId AND StatusId = @statusId;";
            SqlCommand command = new SqlCommand(queryAllEmployeeTickets, connection);
            command.Parameters.AddWithValue("@employeeId", employeeId);
            command.Parameters.AddWithValue("@statusId", statusId);

            try {
                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader()) {
                    if(!reader.HasRows) return null!;
                    while(reader.Read()) {
                        if((int)reader[4] == statusId) {
                            ReimburseTicket newTicket = new ReimburseTicket(
                                (string) reader[0],
                                (string) reader[1],
                                (int) reader[2],
                                (string) reader[3],
                                (int) reader[4],
                                (int) reader[5]
                            );
                            employeeTickets.Add(newTicket);
                        }
                    }
                    Console.WriteLine("GET Success");
                    return employeeTickets;
                }
            } catch(Exception e) {
                Console.WriteLine("GET error\n" + e.Message);
                return null!;
            }
        }
    }

    public List<ReimburseTicket> GetPending(int managerId) {
                string conString = File.ReadAllText("../../ConString.txt");
        List<ReimburseTicket> employeeTickets = new List<ReimburseTicket>();
        using(SqlConnection connection = new SqlConnection(conString)) {
            string queryAllEmployeeTickets = "SELECT * FROM Ticket WHERE StatusId = @statusId;";
            SqlCommand command = new SqlCommand(queryAllEmployeeTickets, connection);
            command.Parameters.AddWithValue("@statusId", 0);

            try {
                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader()) {
                    if(!reader.HasRows) return null!;
                    while(reader.Read()) {
                        if((int)reader[4] == 0) {
                            ReimburseTicket newTicket = new ReimburseTicket(
                                (string) reader[0],
                                (string) reader[1],
                                (int) reader[2],
                                (string) reader[3],
                                (int) reader[4],
                                (int) reader[5]
                            );
                            employeeTickets.Add(newTicket);
                        }
                    }
                    Console.WriteLine("GET Success");
                    return employeeTickets;
                }
            } catch(Exception e) {
                Console.WriteLine("GET error\n" + e.Message);
                return null!;
            }
        }
    }
}