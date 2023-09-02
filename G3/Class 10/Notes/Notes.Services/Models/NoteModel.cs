﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public IEnumerable<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
