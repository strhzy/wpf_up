using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Operation
{
    public int Id { get; set; }

    public string OperationName { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
