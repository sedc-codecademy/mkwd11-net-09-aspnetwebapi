using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.DataAccess.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {

        //This method is custom method for showing sql injection attack
        Note GetByTag(string tag);
    }
}
