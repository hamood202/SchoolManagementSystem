using System;
using System.Collections.Generic;
using Domain;
namespace Domain;

public partial class TbStudent: BaseTable
{
    public string? Name { get; set; }

    public string? Ename { get; set; }

    public string Phone { get; set; } = null!;

    public DateOnly? Birth { get; set; }

    public Guid CityId { get; set; }

    public long? Sex { get; set; }

    public string? Note { get; set; }

    public string? Address { get; set; }
 
    public virtual ICollection<TbAttendance> TbAttendances { get; set; } = new List<TbAttendance>();
}
