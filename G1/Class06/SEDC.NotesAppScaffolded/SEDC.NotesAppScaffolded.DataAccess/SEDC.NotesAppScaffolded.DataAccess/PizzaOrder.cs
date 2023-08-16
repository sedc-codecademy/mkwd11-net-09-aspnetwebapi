using System;
using System.Collections.Generic;

namespace SEDC.NotesAppScaffolded.DataAccess.SEDC.NotesAppScaffolded.DataAccess;

public partial class PizzaOrder
{
    public int Id { get; set; }

    public int PizzaId { get; set; }

    public int OrderId { get; set; }

    public int PizzaSize { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Pizza Pizza { get; set; } = null!;
}
