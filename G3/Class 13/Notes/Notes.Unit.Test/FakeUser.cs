using Notes.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Unit.Test
{
    public class FakeUser
        : ICurrentUser
    {
        public FakeUser(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get;private set; }
    }
}
