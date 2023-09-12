﻿namespace SEDC.NotesAppFinal.Services.Interfaces
{
    using SEDC.NotesAppFinal.DTOs.NoteDTOs;

    public interface INotesService
    {
        Task<NoteDto> GetNoteAsync(int id);

        Task<List<NoteDto>> GetAllNotesAsync();

        Task CreateNoteAsync(CreateNoteDto note);

        Task DeleteNoteAsync(int id);

        Task EditNoteAsync(CreateNoteDto createNoteDto, int id);
    }
}
