using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.DAL.Entities
{
    public class Connection
    {
        public int Id { get; set; }

        public Profile From { get; set; }

        public Profile To { get; set; }
    }
}
