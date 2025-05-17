using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class GradeDto : BaseDto
    {
        public double Value { get; set; }
        public Guid StudentId { get; set; }
        public DateTime DateRecorded { get; set; }
        public Guid SubjectId { get; set; }
    }
}
