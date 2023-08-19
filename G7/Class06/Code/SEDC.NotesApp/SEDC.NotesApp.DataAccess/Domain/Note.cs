using SEDC.NotesApp.DataAccess.Domain.Enums;
using System;
using System.Collections.Generic;

namespace SEDC.NotesApp.DataAccess.Domain;

public partial class Note
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public PriorityEnum Priority { get; set; }

    public TagEnum Tag { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public virtual User User { get; set; } = null!;
}
