using System;

namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }
    }
}
