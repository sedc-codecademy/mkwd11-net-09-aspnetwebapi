using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.User
{
    public interface ICurrentUser
    {
        public int Id { get;  }

        public string Name { get; }
    }
}
