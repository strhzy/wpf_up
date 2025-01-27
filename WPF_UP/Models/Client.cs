using System;
using System.Collections.Generic;

namespace WPF_UP.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string TelephoneNumber { get; set; } = null!;

    public string CarBrand { get; set; } = null!;

    public string CarModel { get; set; } = null!;

    public string GovNumber { get; set; } = null!;

    public DateTime? LastVisitDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
