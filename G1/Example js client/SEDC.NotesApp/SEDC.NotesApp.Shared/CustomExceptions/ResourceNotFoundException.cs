using System;

namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) 
        {
            //log id
        }
    }
}
