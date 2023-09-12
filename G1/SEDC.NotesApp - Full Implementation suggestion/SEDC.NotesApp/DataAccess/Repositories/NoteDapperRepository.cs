using Configurations;
using Dapper;
using Dapper.Contrib.Extensions;
using DataModels;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace DataAccess
{
    public class NoteDapperRepository : IRepository<NoteDto>
    {
        private AppSettings _appSettings;

        public NoteDapperRepository(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public void Delete(NoteDto entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_appSettings.NoteAppConnectionString))
            {
                sqlConnection.Open();

                string sql = "DELETE FROM dbo.Notes WHERE Id = @NoteId";
                sqlConnection.Execute(sql, new { NoteId = entity.Id });
            }
        }

        public IEnumerable<NoteDto> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_appSettings.NoteAppConnectionString))
            {
                sqlConnection.Open();
                List<NoteDto> notesDb = sqlConnection.Query<NoteDto>("SELECT * FROM dbo.Notes").ToList();
                return notesDb;
            }
        }

        public NoteDto GetById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_appSettings.NoteAppConnectionString))
            {
                sqlConnection.Open();

                NoteDto noteDb = sqlConnection.Query<NoteDto>("SELECT * FROM dbo.Notes WHERE Id = @NoteId", new { NoteId = id }).FirstOrDefault();
                return noteDb;
            }
        }

        public void Add(NoteDto entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_appSettings.NoteAppConnectionString))
            {
                sqlConnection.Open();
                string insertQuery = @"INSERT INTO [dbo].[Notes]([Text], [Color], [Tag], [UserId]) VALUES (@Text, @Color, @Tag, @UserId)";

                sqlConnection.Query(insertQuery, new
                {
                    Text = entity.Text,
                    Color = entity.Color,
                    Tag = entity.Tag,
                    UserId = entity.UserId
                });
            }
        }

        public void Update(NoteDto entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_appSettings.NoteAppConnectionString))
            {
                sqlConnection.Open();
                string insertQuery = @"UPDATE [dbo].[Notes] SET Text = @Text, Color = @Color, Tag = @Tag, UserId = @UserId WHERE Id = @Id";

                sqlConnection.Query(insertQuery, new
                {
                    Id = entity.Id,
                    Text = entity.Text,
                    Color = entity.Color,
                    Tag = entity.Tag,
                    UserId = entity.UserId
                });
            }
        }
    }
}
