using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }


        //navigation properties
        public List<Note> Notes { get; set; }
    }
}
