using System;
using System.Collections.Generic;

namespace SEDC.NotesAppScaffolded.DataAccess.SEDC.NotesAppScaffolded.DataAccess;

public partial class Pizza
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsOnPromotion { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();
}
