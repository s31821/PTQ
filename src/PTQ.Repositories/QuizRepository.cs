using Microsoft.Data.SqlClient;
using PTQ.Models;

namespace PTQ.Repositories;

public class QuizRepository
{
    private string _connectionString;
    public QuizRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IEnumerable<Quiz> GetQuizzes()
    {
        List<Quiz> quizzes = [];
        const string queryString = "SELECT * FROM Quiz";

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
                        var quiz = new Quiz
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PotatoTeacherId = reader.GetInt32(2),
                            PathFile = reader.GetString(3)
                        };
                        quizzes.Add(quiz);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return quizzes;
    }

    public Quiz? GetQuizById(int id)
    {
        const string queryString = "SELECT * FROM Quiz WHERE Id = @id";
        Quiz? quiz = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@id", id);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quiz = new Quiz
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PotatoTeacherId = reader.GetInt32(2),
                            PathFile = reader.GetString(3)
                        };
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return quiz;
    }
}