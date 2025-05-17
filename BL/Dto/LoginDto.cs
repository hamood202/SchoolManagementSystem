using AppResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [EmailAddress(ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(MessagesAr))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [MinLength(8, ErrorMessageResourceName = "PasswordMinLength", ErrorMessageResourceType = typeof(MessagesAr))]
        public string Password { get; set; }
    }
}
