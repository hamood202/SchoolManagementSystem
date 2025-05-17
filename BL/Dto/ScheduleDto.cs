using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class ScheduleDto: BaseDto
    {
        public int Day { get; set; } 
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid SubjectId { get; set; }
        public Guid ClassId { get; set; }
    }
}
