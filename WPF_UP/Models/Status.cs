﻿using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Status
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
