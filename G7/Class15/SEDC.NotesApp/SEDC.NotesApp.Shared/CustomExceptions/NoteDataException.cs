using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class NoteDataException : Exception
    {
        public NoteDataException(string message) : base(message)
        {

        }
    }
}
