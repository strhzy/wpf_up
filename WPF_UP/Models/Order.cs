using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Order
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime DateReference { get; set; }

    public string Description { get; set; } = null!;

    public DateTime? RepairDate { get; set; }

    public int StatusId { get; set; }

    public int ServiceStationId { get; set; }

    public decimal Price { get; set; }

    public int EmployeeId { get; set; }

    public int OperationId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;

    public virtual ICollection<OrderSparePart> OrderSpareParts { get; set; } = new List<OrderSparePart>();

    public virtual ServiceStation ServiceStation { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
