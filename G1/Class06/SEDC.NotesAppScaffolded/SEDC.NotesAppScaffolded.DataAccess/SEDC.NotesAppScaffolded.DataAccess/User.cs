using System;
using System.Collections.Generic;

namespace SEDC.NotesAppScaffolded.DataAccess.SEDC.NotesAppScaffolded.DataAccess;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
