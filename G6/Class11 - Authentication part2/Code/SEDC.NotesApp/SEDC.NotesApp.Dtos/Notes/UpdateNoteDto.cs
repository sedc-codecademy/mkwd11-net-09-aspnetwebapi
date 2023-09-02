using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Dtos.Notes
{
    public class UpdateNoteDto : AddNoteDto
    {
        public int Id { get; set; }
    }
}
