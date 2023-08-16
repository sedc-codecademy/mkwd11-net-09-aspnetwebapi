﻿using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class UpdateNoteDto
    {
        //DTO = data transfer object
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<int> TagIds { get; set; }
    }
}
