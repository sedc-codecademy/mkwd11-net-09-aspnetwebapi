using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notes.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public class DapperNotesRepository : INotesRepository
    {
        private readonly IConfiguration configuration;

        public DapperNotesRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IEnumerable<Note> GetAll()
        {
            var connectionString = configuration.GetConnectionString("NotesConnection");
            using var connection = new SqlConnection(connectionString);

            var notes = connection.Query<Note>(@"Select 
                Id, 
                Title, 
                Description,
                t.Id as TagId,
                t.Name as TagName
            From 
                Notes
            Left join 
                tag t 
            on 
                Notes.Id = t.NoteId");
            return notes;
        }
        public void Create(Note entity)
        {
            var connectionString = configuration.GetConnectionString("NotesConnection");
            using var connection = new SqlConnection(connectionString);
            connection.Execute(@"INSERT INTO NOTES (Title,Description), VALUES(@Title, @Description)", 
            new
            {
                entity.Title,
                entity.Description
            });
        }

        public Note? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> GetNotes(string? title, string? description, string orderBy = "Title", bool isAsc = true)
        {
            throw new NotImplementedException();
        }

        public void Remove(Note entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> GetNotes(int userId, string? title, string? description, string orderBy = "Title", bool isAsc = true)
        {
            throw new NotImplementedException();
        }
    }
}
