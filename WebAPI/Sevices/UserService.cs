using BL.Interfase;
using BL.Dto;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DataAccessLayer.UserModel;

namespace WebAPIi.Sevices
{
    public class UserService : IUserService
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly IHttpContextAccessor _httpAccessor;
        public UserService(UserManager<ApplicationUser> userService, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpAccessor)
        {
            _userManager = userService;
            _signInManager = signInManager;
            _httpAccessor = httpAccessor;
        }

        public async Task<UserResultDto> RegisterUser(UserDto registerDto)
        {
            if(registerDto.Password != registerDto.ConfirmPassword)
            {
                return new UserResultDto
                {
                    Success = false,
                    Error = new[] { "Passwords do not match." }
                };
            }
            var user = new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            return new UserResultDto
            {
                Success = result.Succeeded,
                Error = result.Errors.Select(e => e.Description)
            };
         
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new UserResultDto
                {
                    Success = false,
                    Error = new[] { "Invalid login attempt. Please check your email and password." }

                };
            }

            return new UserResultDto
            {
                Success = true,
                Token = "dummy_token", // Replace with actual token generation logic
            };

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(string UseId)
        {
            var user = await _userManager.FindByIdAsync(UseId);
            if (user == null)            
                return null;            

           return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                // Add other properties as needed
            };          
        }
       
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)            
                return null;            

           return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                // Add other properties as needed
            };          
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = _userManager.Users;

           return users.Select(user => new UserDto
            {
                Id =Guid.Parse(user.Id),
                Email = user.Email              
                // Add other properties as needed
            });
        }

        public Guid GetLoggedInUser()
        {
            var userId = _httpAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Guid.Empty;

            return Guid.Parse(userId);
        }
    }
}
