using Microsoft.Data.SqlClient;
using PTQ.Models;

namespace PTQ.Repositories;


public class PotatoTeacherRepository
{   
    private string _connectionString;
    public PotatoTeacherRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<PotatoTeacher> GetPotatoTeachers()
    {
        List<PotatoTeacher> potatoTeachers = [];
        const string queryString = "SELECT * FROM PotatoTeacher";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var potatoTeacher = new PotatoTeacher
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        };
                        potatoTeachers.Add(potatoTeacher);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return potatoTeachers;
    }
}