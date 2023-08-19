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
    public class AdoNetNotesRepository : INotesRepository
    {
        private readonly IConfiguration configuration;

        public AdoNetNotesRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IEnumerable<Note> GetAll()
        {
            var connectionString = configuration.GetConnectionString("NotesConnection");
            using var connection = new SqlConnection(connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"
            Select 
                Id, 
                Title, 
                Description,
                t.Id as TagId,
                t.Name
            From 
                Notes
            Left join 
                tag t 
            on 
                Notes.Id = t.NoteId";
            SqlDataReader? reader = command.ExecuteReader();
            var notes = new List<Note>();
            Note currentNote = null;
            var tags = new List<Tag>();

            while (reader.Read())
            {
                if (currentNote != null && currentNote.Id != (int)reader["Id"])
                {
                    currentNote.Tags = tags;
                    tags.Clear();
                    notes.Add(currentNote);
                    currentNote = null;
                }
                if (currentNote == null)
                {
                    currentNote = new Note
                    {
                        Id = (int)reader["Id"],
                        Description = reader["Description"].ToString(),
                        Title = reader["Title"].ToString()
                    };
                }

                tags.Add(new Tag
                {
                    Id = (int)reader["TagId"],
                    Name = reader["Name"].ToString()
                });
            }
            return notes;
        }
        public void Create(Note entity)
        {
            var connectionString = configuration.GetConnectionString("NotesConnection");
            using var connection = new SqlConnection(connectionString);
            var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO NOTES (Title,Description), VALUES(@title, @description)";
            command.Parameters.AddWithValue("@title", entity.Title);
            command.Parameters.AddWithValue("@description", entity.Description);
            var result = command.ExecuteNonQuery();
            transaction.Commit();
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
    }
}
