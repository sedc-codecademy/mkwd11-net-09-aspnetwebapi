﻿using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
