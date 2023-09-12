using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess.Implementation
{
    public class NoteDapperRepository : IRepository<Note>, INoteRepository
    {
        private string sqlConnectionString = "Server=.;Database=noteappdb;Trusted_Connection=True;Encrypt=False";
        private NotesAppDbContext _dbContext;

        public NoteDapperRepository(NotesAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Note entity)
        {
            //_dbContext.Database.GetConnectionString(); get connection string from dbContext
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                string sqlQuery = @"INSERT INTO [dbo].[Notes]
                                           ([Text]
                                           ,[Priority]
                                           ,[Tag]
                                           ,[UserId])
                                     VALUES
                                           (@text
                                           ,@prio
                                           ,@tag
                                           ,@userId)";

                sqlConnection.Query(sqlQuery, new
                {
                    text = entity.Text,
                    prio = entity.Priority,
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
            using(SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                List<Note> result = sqlConnection.Query<Note>("Select * From dbo.Notes").ToList();

                return result;
            }
        }

        public Note GetById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();

                //Note note = sqlConnection.QueryFirstOrDefault<Note>($"Select * From dbo.Notes Where Id = @NoteId", new { NoteId = id});
                Note note = sqlConnection.Query<Note>($"Select * From dbo.Notes Where Id = @NoteId", new { NoteId = id }).FirstOrDefault();

                return note;
            }
        }

        public Note GetByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            _dbContext.Notes.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
