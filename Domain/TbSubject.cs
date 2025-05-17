using Domain;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class TbSubject :BaseTable
{

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int CreditHours { get; set; }

 
}
