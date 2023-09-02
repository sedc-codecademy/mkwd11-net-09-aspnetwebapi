using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SEDC.NoteApp.Domain.Enums;
using SEDC.NoteApp.Domain.Models;

namespace SEDC.NoteApp.DataAccess.AdoNetImplementation
{
    // SqlConnection => used to establish connection to a database
    // SqlCommand => execute SQL queries, stored procedures, and other db commands
    // SqlDataReader => read data from a database
    public class NoteAdoNetRepository : IRepository<Note>
    {
        private readonly string _connectionString;
        // we initialize private fields only if we use them in multiple cases
        //private readonly IConfiguration _configuration;
        public NoteAdoNetRepository(IConfiguration configuration)
        {
            //_configuration = configuration; 
            //_connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Note> GetAll()
        {
            var notes = new List<Note>();
            // 1. Establish the connection to the Database
            // bad way
            //sqlConnection.Open();
            // code....
            //sqlConnection.Close();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                // 2. Create SQL query
                //var query = "SELECT * FROM Notes";
                var query = @"SELECT n.Id, n.Priority, n.Tag, n.Text, n.UserId 
                              FROM Notes n";
                // 3. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                // 4. Execute the sql command
                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                // 5. Read the data from the executed query
                while (sqlDataReader.Read()) 
                {
                    var note = new Note()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Priority = (Priority)sqlDataReader.GetInt32(1),
                        Tag = (Tag)sqlDataReader["Tag"],
                        Text = sqlDataReader.GetString(3),
                        UserId = sqlDataReader.GetInt32(4),
                        //User = new User
                        //{
                        //    Id = sqlDataReader.GetInt32(5),
                        //}
                    };
                    notes.Add(note);
                }
            }
            return notes;
        }

        public Note GetById(int id)
        {
            // 1. Establish the connection to the Database
            // bad way
            //sqlConnection.Open();
            // code....
            //sqlConnection.Close();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                // 2. Create SQL query
                //var query = "SELECT * FROM Notes";
                var query = @$"SELECT n.Id, n.Priority, n.Tag, n.Text, n.UserId 
                              FROM Notes n
                              WHERE n.Id = @NoteIdentificator";
                // 3. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@NoteIdentificator", id);
                // 4. Execute the sql command
                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                // 5. Read the data from the executed query
                if (sqlDataReader.Read())
                {
                    return new Note()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Priority = (Priority)sqlDataReader.GetInt32(1),
                        Tag = (Tag)sqlDataReader["Tag"],
                        Text = sqlDataReader.GetString(3),
                        UserId = sqlDataReader.GetInt32(4),
                        //User = new User
                        //{
                        //    Id = sqlDataReader.GetInt32(5),
                        //}
                    };
                }
                return null;
            }
        }

        public void Add(Note entity)
        {
            // 1. Establish the connection to the Database
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var query = @"INSERT INTO dbo.Notes (Text, Priority, Tag, UserId)
                              VALUES (@text, @priority, @tag, @userId)";
                // 2. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                // 3. Map the values
                sqlCommand.Parameters.AddWithValue("@text", entity.Text);
                sqlCommand.Parameters.AddWithValue("@priority", entity.Priority);
                sqlCommand.Parameters.AddWithValue("@tag", entity.Tag);
                sqlCommand.Parameters.AddWithValue("@userId", entity.UserId);
                // 4. Execute the query
                var rowsAffected = sqlCommand.ExecuteNonQuery();
            }
        }

        public void Update(Note entity)
        {
            // 1. Establish the connection to the Database
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var query = @"UPDATE dbo.Notes 
                              SET Text = @text, Priority = @priority, 
                              Tag = @tag, UserId = @userId
                              WHERE Id = @noteId";
                // 2. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                // 3. Map the values
                sqlCommand.Parameters.AddWithValue("@text", entity.Text);
                sqlCommand.Parameters.AddWithValue("@priority", entity.Priority);
                sqlCommand.Parameters.AddWithValue("@tag", entity.Tag);
                sqlCommand.Parameters.AddWithValue("@userId", entity.UserId);
                sqlCommand.Parameters.AddWithValue("@noteId", entity.Id);
                // 4. Execute the query
                var rowsAffected = sqlCommand.ExecuteNonQuery();
            }
        }

        public void Delete(Note entity)
        {
            // NOTE: in real case scenario we don't/rarely perform HARD delete to records in our Database, instead we use SOFT delete (ex. set model's property IsDeleted to true)
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var query = @"DELETE FROM dbo.Notes 
                              WHERE Id = @noteId";
                // 2. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                // 3. Map the values
                sqlCommand.Parameters.AddWithValue("@noteId", entity.Id);
                // 4. Execute the query
                sqlCommand.ExecuteNonQuery();
                //var rowsAffected = sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
