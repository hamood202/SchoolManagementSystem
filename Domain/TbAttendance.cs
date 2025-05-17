using Domain;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class TbAttendance: BaseTable
{

    public Guid StudentId { get; set; }

    public DateOnly Date { get; set; }

    public bool IsPresent { get; set; }

    public string? Notes { get; set; }

  
    public virtual TbStudent Student { get; set; } = null!;
}
