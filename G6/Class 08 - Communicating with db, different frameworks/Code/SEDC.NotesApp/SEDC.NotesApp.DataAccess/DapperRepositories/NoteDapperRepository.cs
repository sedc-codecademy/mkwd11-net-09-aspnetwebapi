using Dapper;
using Microsoft.Data.SqlClient;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.DataAccess.DapperRepositories
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteDapperRepository()
        {
            _connectionString = "Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;TrustServerCertificate=True";
        }

        public void Add(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string insertQuery = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId) " +
                    "VALUES(@text, @priority, @tag, @userId)";

                sqlConnection.Execute(insertQuery, new
                {
                    text = entity.Text,
                    priority = entity.Priority,
                    tag = entity.Tag,
                    userId = entity.UserId
                });
            }
        }

        public void Delete(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string deleteQuery = "DELETE FROM dbo.Notes WHERE Id = @id";
                sqlConnection.Execute(deleteQuery, new { id = entity.Id });
            }
        }

        public List<Note> GetAll()
        {
            //we need to create and open sql connection
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                List<Note> notesDb = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes").ToList();
                return notesDb;
            }
        }

        public Note GetById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                Note noteDb = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes WHERE Id = @NoteId", new { NoteId = id }).FirstOrDefault();
                return noteDb;
            }
        }

        public void Update(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string updateQuery = "UPDATE dbo.Notes SET Text = @text, Tag = @tag, Priority = @priority, UserId = @userId" +
                    " WHERE Id = @id";

                sqlConnection.Execute(updateQuery, new
                {
                    text = entity.Text,
                    priority = entity.Priority,
                    tag = entity.Tag,
                    userId = entity.UserId,
                    id = entity.Id
                });
            }
        }
    }
}
