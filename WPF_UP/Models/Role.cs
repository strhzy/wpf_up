using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<EmployeeAccount> EmployeeAccounts { get; set; } = new List<EmployeeAccount>();
}
