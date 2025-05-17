using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class UserResultDto:BaseDto
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Error { get; set; }

    }
}
