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

        public virtual Profile From { get; set; }

        public virtual Profile To { get; set; }
    }
}
