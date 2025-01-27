using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class OrderSparePart
{
    public int Id { get; set; }

    public int SparePartId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual SparePart SparePart { get; set; } = null!;
}
