﻿using SEDC.NotesApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Dtos.Notes
{
    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public TagEnum Tag { get; set; }
        public int UserId { get; set; }
    }
}
