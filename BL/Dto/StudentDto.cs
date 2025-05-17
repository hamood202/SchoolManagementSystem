using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class StudentDto: BaseDto
    {
        public string? Name { get; set; }

        public string? Ename { get; set; }

        public string Phone { get; set; } = null!;

        public DateOnly? Birth { get; set; }

        public Guid CityId { get; set; }

        public long? Sex { get; set; }

        public string? Note { get; set; }

        public string? Address { get; set; }
    }
}
