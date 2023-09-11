using System;

namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class NoteException : Exception
    {
        public NoteException(string message) : base(message)
        {

        }
    }
}
