using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TbGrade : BaseTable
    {
        public double Value { get; set; }
        public Guid StudentId { get; set; }
        public DateTime DateRecorded { get; set; }
        public Guid SubjectId { get; set; }
    
    }
}
