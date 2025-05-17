using Domain;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class TbTeacher: BaseTable
{

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly? Birth { get; set; }

    public Guid CityId { get; set; }

    public long? Sex { get; set; }

    public string? Note { get; set; }

    public string? Address { get; set; }

    public string Specialization { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public string? Qualification { get; set; }

    public int? YearsOfExperience { get; set; }

    public virtual TbCity IdNavigation { get; set; } = null!;

    public virtual ICollection<TbTeacherSubject> TbTeacherSubjects { get; set; } = new List<TbTeacherSubject>();
}
