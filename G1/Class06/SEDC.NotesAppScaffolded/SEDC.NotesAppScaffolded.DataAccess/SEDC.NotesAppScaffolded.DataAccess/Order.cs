using System;
using System.Collections.Generic;

namespace SEDC.NotesAppScaffolded.DataAccess.SEDC.NotesAppScaffolded.DataAccess;

public partial class Order
{
    public int Id { get; set; }

    public int PaymentMethod { get; set; }

    public bool IsDelivered { get; set; }

    public string Location { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();

    public virtual User User { get; set; } = null!;
}
