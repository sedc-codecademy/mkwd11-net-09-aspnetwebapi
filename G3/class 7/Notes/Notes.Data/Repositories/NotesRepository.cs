using Microsoft.EntityFrameworkCore;
using Notes.Data.Data;
using Notes.Data.Domain;
using System.Linq.Expressions;

namespace Notes.Data.Repositories
{
    public class NotesRepository : BaseRepository<Note>, INotesRepository
    {
        public NotesRepository(NotesDbContext notesDbContext)
            : base(notesDbContext)
        {
        }

        public IEnumerable<Note> GetNotes(string? title, string? description, string orderBy = nameof(Note.Title), bool isAsc = true)
        {
            IQueryable<Note> query = notesDbContext.Notes.Include(x => x.Tags);
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(x => x.Description.Contains(description));
            }

            Expression<Func<Note, object>>? orderByExpression = null;
            if(orderBy.ToLower() == nameof(Note.Description).ToLower())
            {
                orderByExpression = note => note.Description;
            }
            else
            {
                orderByExpression = note => note.Title;
            }
            if (isAsc)
            {
                query = query.OrderBy(orderByExpression);
            }
            else
            {
                query = query.OrderByDescending(orderByExpression);
            }
            return query.ToList();
        }
    }
}
