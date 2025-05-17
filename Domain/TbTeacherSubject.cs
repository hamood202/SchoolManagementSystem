using Domain;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class TbTeacherSubject : BaseTable
{
    public Guid TeacherId { get; set; }
    public Guid SubjectId { get; set; } 
    public virtual TbTeacher Teacher { get; set; } = null!;
}
