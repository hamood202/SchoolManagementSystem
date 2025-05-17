using System;
using BL.Dto.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using AppResource;

namespace BL.Dto
{
    public class UserDto : BaseDto
    {
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [EmailAddress(ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(MessagesAr))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [MinLength(8, ErrorMessageResourceName = "PasswordMinLength", ErrorMessageResourceType = typeof(MessagesAr))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [StringLength(50, ErrorMessageResourceName = "FirstNameLength", ErrorMessageResourceType = typeof(MessagesAr))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [StringLength(50, ErrorMessageResourceName = "LastNameLength", ErrorMessageResourceType = typeof(MessagesAr))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "PhoneRequired", ErrorMessageResourceType = typeof(MessagesAr))]
        [Phone(ErrorMessageResourceName = "PhoneInvalid", ErrorMessageResourceType = typeof(MessagesAr))]
        public string Phone { get; set; }

        [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordMismatch", ErrorMessageResourceType = typeof(MessagesAr))]
        public string? ConfirmPassword { get; set; }
        public string? Role { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
