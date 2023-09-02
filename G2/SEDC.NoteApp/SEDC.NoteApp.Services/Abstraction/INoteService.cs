using SEDC.NoteApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Services.Abstraction
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes(int userId);
        NoteDto GetById(int id);
        void AddNote(AddNoteDto addNoteDto);
        void UpdateNote(UpdateNoteDto updateNoteDto);
        void DeleteNote(int id);

    }
}
