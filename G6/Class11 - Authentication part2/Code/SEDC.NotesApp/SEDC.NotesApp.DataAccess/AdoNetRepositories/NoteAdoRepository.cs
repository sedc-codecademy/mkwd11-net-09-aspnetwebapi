using Microsoft.Data.SqlClient;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SEDC.NotesApp.DataAccess.AdoNetRepositories
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteAdoRepository()
        {
            _connectionString = "Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;TrustServerCertificate=True";
        }
        public void Add(Note entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                          "VALUES(@text, @priority, @tag, @userId)";

                sqlCommand.Parameters.AddWithValue("@text", entity.Text);
                sqlCommand.Parameters.AddWithValue("@priority", entity.Priority);
                sqlCommand.Parameters.AddWithValue("@tag", entity.Tag);
                sqlCommand.Parameters.AddWithValue("@userId", entity.UserId);

                //sqlCommand.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                //                          $"VALUES({entity.Text}, @priority, @tag, @userId)";

                sqlCommand.ExecuteNonQuery();

            } //here the connection object will be disposed and the connection will be closed
        }

        public void Delete(Note entity)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            //4. connect the command 
            command.Connection = sqlConnection;

            command.CommandText = "DELETE FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<Note> GetAll()
        {
            //1. Create SQL Connection
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            //2. Open the connection
            sqlConnection.Open();

            //3.Create sql command that will contain sql query
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

           // sqlCommand.CommandText = "SELECT * FROM dbo.Notes n inner join Users u";
            sqlCommand.CommandText = "SELECT * FROM dbo.Notes";

            //execute the command and read data
            List<Note> notesDb = new List<Note>();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                notesDb.Add(new Note
                {
                    Id = (int)reader["Id"],
                    Tag = (Tag)reader["Tag"],
                    Priority = (Priority)reader["Priority"],
                    Text = (string)reader["Text"],
                    UserId = (int)reader["UserId"],
                    //User = new User
                    //{
                    //    FirstName = (string)reader["FirstName"]
                    //}
                });
            }

            //Close the connection!!!
            sqlConnection.Close();

            return notesDb;
        }

        public Note GetById(int id)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            //command.CommandText = $"SELECT * FROM dbo.Notes WHERE Id = {id}";
            command.CommandText = "SELECT * FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            List<Note> notesDb = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (Priority)sqlDataReader["Priority"],
                    Tag = (Tag)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                    //User = new User
                    //{
                    //    FirstName = (string)sqlDataReader["FirstName"]
                    //}
                });
            }

            //6.close the connection!!!
            sqlConnection.Close();

            return notesDb.FirstOrDefault();
        }

        public void Update(Note entity)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            command.CommandText = "UPDATE dbo.Notes SET Text = @text, Tag = @tag, Priority = @priority, UserId = @userId" +
                " WHERE Id = @id";

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}
