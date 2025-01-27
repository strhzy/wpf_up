using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Qualification
{
    public int Id { get; set; }

    public string QualificationName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
