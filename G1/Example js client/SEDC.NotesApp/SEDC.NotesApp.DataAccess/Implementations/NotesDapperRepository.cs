using Dapper;
using Dapper.Contrib.Extensions;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.NotesApp.DataAccess.Implementations
{
    public class NotesDapperRepository : IRepository<Note>
    {
        private string _connectionString;

        public NotesDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Delete(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                //sqlConnection.Delete<Note>(entity);

                string sql = "DELETE FROM dbo.Notes WHERE Id = @NoteId";
                sqlConnection.Execute(sql, new { NoteId = entity.Id });
            }
         }

        public List<Note> GetAll()
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                List<Note> notesDb = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes").ToList();
                return notesDb;
            }
        }

        public Note GetById(int id)
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                Note noteDb = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes WHERE Id = @NoteId", new { NoteId = id }).FirstOrDefault();
                return noteDb;
            }
        }

        public void Insert(Note entity)
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Insert<Note>(entity);
            }
        }

        public void Update(Note entity)
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Update<Note>(entity);
            }
        }
    }
}
