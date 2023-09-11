using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Shared.Shared
{
    public class DataException : Exception
    {
        public DataException(string message) : base(message)
        {

        }
    }
}
