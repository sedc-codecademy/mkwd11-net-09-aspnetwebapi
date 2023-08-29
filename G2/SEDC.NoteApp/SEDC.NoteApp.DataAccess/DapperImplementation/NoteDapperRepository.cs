using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SEDC.NoteApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.DataAccess.DapperImplementation
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private readonly string _connectionString;
        public NoteDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Note> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM dbo.Notes";
                return sqlConnection.Query<Note>(query).ToList();
            }
        }

        public Note GetById(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.Notes WHERE Id = @noteId";
            var noteFromDb = sqlConnection.QueryFirstOrDefault<Note>(query, new { noteId = id });
            return noteFromDb;
        }

        //public void Add(Note entity)
        //{
        //    using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        //    //sqlConnection.Insert(entity);
        //    var query = @"INSERT INTO dbo.Notes (Text, Priority, Tag, UserId) VALUES (@Text, @Priority, @Tag, @UserId)";
        //    var rowsAffected = sqlConnection.Execute(query, entity);
        //}

        // Add new Note by calling stored procedure
        public void Add(Note entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            var query = @"EXEC dbo.SP_AddNote @Text, @Priority, @Tag, @UserId";
            var rowsAffected = sqlConnection.Execute(query, entity);
        }

        public void Update(Note entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            var query = @"UPDATE dbo.Notes 
                        SET Text = @Text, Priority = @Priority, 
                        Tag = @Tag, UserId = @UserId
                        WHERE Id = @Id";
            sqlConnection.Execute(query, entity);
        }

        public void Delete(Note entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Execute("DELETE FROM Notes WHERE Id = @Id", new { Id = entity.Id });
        }

    }
}
