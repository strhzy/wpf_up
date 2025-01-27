using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int PositionId { get; set; }

    public int QualificationId { get; set; }

    public int ServiceStationId { get; set; }

    public int EmployeeAccountId { get; set; }

    public virtual EmployeeAccount EmployeeAccount { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Position Position { get; set; } = null!;

    public virtual Qualification Qualification { get; set; } = null!;

    public virtual ServiceStation ServiceStation { get; set; } = null!;
}
