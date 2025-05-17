using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class SubjectDto: BaseDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int CreditHours { get; set; }

    }
}
