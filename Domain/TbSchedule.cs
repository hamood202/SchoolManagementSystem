using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TbSchedule : BaseTable
    {

       
        public int Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ClassId { get; set; }
        public virtual TbSubject Subject { get; set; }
        public virtual TbClass Class { get; set; }
    }
}
