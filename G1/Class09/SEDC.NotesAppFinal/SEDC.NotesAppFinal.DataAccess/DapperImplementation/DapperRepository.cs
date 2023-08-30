using Dapper;
using Microsoft.Data.SqlClient;
using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Models;

namespace SEDC.NotesAppFinal.DataAccess.DapperImplementation
{
    public class DapperRepository : IRepository<Note>
    {
        private string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateAsync(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string insertQuery = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                     "VALUES (@text, @priority, @tag, @userId)";

                await sqlConnection.QueryAsync(insertQuery, new
                {
                    text = entity.Text,
                    priority = entity.Priority,
                    tag = entity.Tag,
                    userId = entity.UserId
                });
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string deleteQuery = "DELETE FROM dbo.Notes WHERE Id = @id";
                await sqlConnection.ExecuteAsync(deleteQuery, new { id = id });
            }
        }

        public async Task<List<Note>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string selectQuery = "SELECT * FROM dbo.Notes";
                IEnumerable<Note> notesDb = await sqlConnection.QueryAsync<Note>(selectQuery);

                return notesDb.ToList();
            }
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string selectQuery = "SELECT * FROM dbo.Notes WHERE Id = @id";
                IEnumerable<Note> noteDb = await sqlConnection.QueryAsync<Note>(selectQuery, new { id = id });

                return noteDb.ToList().FirstOrDefault();
            }
        }

        public Task UpdateAsync(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
