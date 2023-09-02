using Notes.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public interface INotesRepository : IRepository<Note>
    {
        public IEnumerable<Note> GetNotes(string? title, string? description, string orderBy = nameof(Note.Title), bool isAsc = true);
    }
}
