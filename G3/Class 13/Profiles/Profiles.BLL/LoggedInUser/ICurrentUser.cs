using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.BLL.LoggedInUser
{
    public interface ICurrentUser
    {
        public int Id { get;  }

        public string Name { get;}

        public bool IsInRole(string role);
    }
}
