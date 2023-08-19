using System;
using System.Collections.Generic;

namespace SEDC.NoteScaffoldedApp.DataAccess.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? HomeAddress { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
