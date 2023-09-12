using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notes.Data.Domain;


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
            command.CommandText = @$"
            Select 
                Id as {nameof(FlatNoteTag.Id)}, 
                Title, 
                Description,
                t.Id as TagId,
                t.Name
            From 
                Notes
            Left join 
                tags t 
            on 
                Notes.Id = t.NoteId";
            SqlDataReader? reader = command.ExecuteReader();
            var notes = new List<FlatNoteTag>();

            while (reader.Read())
            {
                notes.Add(new FlatNoteTag
                {
                    Id = (int)reader[nameof(FlatNoteTag.Id)],
                    Name = (string)reader["Name"],
                    Description = (string)reader["Description"],
                    TagId = (int)reader["TagId"],
                    Title = (string)reader["Title"]
                });
            }
            return notes.GroupBy(x => x.Id).Select(x =>
            {
                var note = new Note
                {
                    Id = x.First().Id,
                    Description = x.First().Description,
                    Title = x.First().Title,
                    Tags = x.Select(t => new Tag
                    {
                        Id = t.Id,
                        Name = t.Name,
                    }).ToList()
                };
                return note;
            });
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

        public IEnumerable<Note> GetNotes(int userId, string? title, string? description, string orderBy = "Title", bool isAsc = true)
        {
            throw new NotImplementedException();
        }
    }
}

public class FlatNoteTag
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public int TagId { get; set; }

    public string Name { get; set; }
}