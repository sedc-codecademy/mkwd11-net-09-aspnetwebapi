using System;
using System.Collections.Generic;

namespace SEDC.NotesApp.DataAccess.Domain;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public int Age { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
