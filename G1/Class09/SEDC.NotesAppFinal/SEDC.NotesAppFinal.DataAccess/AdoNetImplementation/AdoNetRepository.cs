using Microsoft.Data.SqlClient;
using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Enums;
using SEDC.NotesAppFinal.Domain.Models;

namespace SEDC.NotesAppFinal.DataAccess.AdoNetImplementation
{
    public class AdoNetRepository : IRepository<Note>
    {
        private string _connectionString;

        public AdoNetRepository(string connectionString)
        {
            _connectionString = connectionString;   
        }

        public async Task CreateAsync(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand();

            command.Connection = sqlConnection;

            command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                  "VALUES (@text, @priority, @tag, @userId)";

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);

            command.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public async Task DeleteAsync(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand();

            command.Connection = sqlConnection;

            command.CommandText = "DELETE FROM dbo.Notes WHERE Id = @id";

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand();

            command.Connection = sqlConnection;

            command.CommandText = "SELECT * FROM dbo.Notes";

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
                });
            }

            await sqlConnection.CloseAsync();

            return notesDb;
        }

        public Task<Note> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
