﻿using SEDC.NotesAppFluentApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppFluentApi.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public TagEnum Tag { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
