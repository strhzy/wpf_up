using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class SparePart
{
    public int Id { get; set; }

    public string PartName { get; set; } = null!;

    public string Articul { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderSparePart> OrderSpareParts { get; set; } = new List<OrderSparePart>();
}
