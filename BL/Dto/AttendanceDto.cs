using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class AttendanceDto
    {
        public Guid StudentId { get; set; }

        public DateOnly Date { get; set; }

        public bool IsPresent { get; set; }

        public string? Notes { get; set; }
    }
}
