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
    List<ReimburseTicket> GetTickets();
    void PostTickets(List<ReimburseTicket> ticketDB);

    ReimburseTicket PostTicket(string guid, string r, int a, string d, int eId);
    ReimburseTicket GetTicket(string ticketId);
}

public class TicketRepository : ITicketRepository {
    // TODO Deprecate
    public List<ReimburseTicket> GetTickets() {
        if(File.Exists("TicketDatabase.json")) {
            return JsonSerializer.Deserialize<List<ReimburseTicket>>(File.ReadAllText("TicketDatabase.json"))!;
        } else {
            return new List<ReimburseTicket>();
        }
    }
    // TODO Deprecate
    public void PostTickets(List<ReimburseTicket> ticketDb) {
        string serializedDb = JsonSerializer.Serialize(ticketDb);
        File.WriteAllText("TicketDatabase.json", serializedDb);
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
                    return GetTicket(guid); // TODO MAKE TICKET ID A KEY TO BE ABLE TO ACCESS?
                }
            } catch(Exception e) {
                Console.WriteLine("Insertion Failure\n" + e.Message);
            }
        }
        
        return null!;
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
}