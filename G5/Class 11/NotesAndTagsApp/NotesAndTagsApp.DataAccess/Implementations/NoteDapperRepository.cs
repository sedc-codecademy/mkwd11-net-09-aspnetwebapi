using Dapper;
using Microsoft.Data.SqlClient;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementations
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private string _connectonString;

        public NoteDapperRepository(string connectonString)
        {
            _connectonString = connectonString;
        }

        public void Add(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectonString))
            {
                sqlConnection.Open();

                var insertQuery = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                   "VALUES(@text, @priority, @tag, @userId)";

                sqlConnection.Query(insertQuery, new
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
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectonString))
            {
                sqlConnection.Open();
                List<Note> notesDb = sqlConnection.Query<Note>("SELECT * FROM dbo.Notes N INNER JOIN dbo.Users U ON U.Id = N.UserId ").ToList();
                return notesDb;
            }

        }

        public Note GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
