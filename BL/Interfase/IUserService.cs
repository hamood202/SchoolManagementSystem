using BL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfase

{
    public interface IUserService
    {        
        Task<UserResultDto> RegisterUser(UserDto registerDto);
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
         Task LogoutAsync();
        Task<UserDto> GetUserByIdAsync(string UseId); 
        Task<UserDto> GetUserByEmailAsync(string UseId); 
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Guid GetLoggedInUser();
    }
}
