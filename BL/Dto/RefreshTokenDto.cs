using BL.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class RefreshTokenDto : BaseDto
    {
        public string Tokens { get; set; }
        public string UserId { get; set; }
        public DateTime Expires { get; set; }
        public int CurrentState { get; set; }
    }
}
