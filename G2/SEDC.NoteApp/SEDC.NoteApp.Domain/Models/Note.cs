using SEDC.NoteApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Domain.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
    }
}
