using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.CustomExceptions
{
    public class NoteDataException : Exception
    {
        public NoteDataException() : base("Generic Note Data exception occurred.")
        {}
        public NoteDataException(string message) : base(message)
        {}

    }
}
