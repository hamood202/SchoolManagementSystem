using Domain;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class TbCity : BaseTable
{

    public string? CityAname { get; set; }

    public virtual TbTeacher? TbTeacher { get; set; }
}
