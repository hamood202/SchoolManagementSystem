using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class TeacherSubjectDto : BaseDto
    {
        public Guid TeacherId { get; set; }

        public Guid SubjectId { get; set; }

    }
}
