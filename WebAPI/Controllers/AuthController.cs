using BL.Interfase;
using BL.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;
using WebAPIi.Sevices;
using WebAPI.Sevices;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IRefreshToken _refreshTokenService;
        public AuthController(IUserService userService, TokenService tokenService, IRefreshToken refreshTokenService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto registerDto)
        {
            var result = await _userService.RegisterUser(registerDto);
            if (!result.Success)
            {
                return BadRequest(new { message = "User registration failed.", errors = result.Error });
            }
            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var userResult = await _userService.LoginAsync(request);

            if (!userResult.Success)
            {
                return Unauthorized("InVailed Credentials.");
            }

            var userData = await GetClaim(request.Email);
            var claims = userData.Item1;
            UserDto user = userData.Item2;

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenDto = new RefreshTokenDto
            {
                Tokens = refreshToken,
                UserId = user.Id.ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CurrentState = 1,
            };
            //      Console.WriteLine(JsonConvert.SerializeObject(refreshTokenDto));

          //  _refreshTokenService.Refresh(refreshTokenDto);

            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = refreshTokenDto.Expires,
            });
            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refreshAccessToken")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] UserDto request)
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }

            var storeToken = _refreshTokenService.GetByToken(refreshToken);

            if (storeToken == null || storeToken.CurrentState == 2 || storeToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or Expires refresh token.");
            }

            var claims = await GetClaimById(storeToken.UserId);

            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            return Ok(new { AccessToken = newAccessToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            // Check if the refresh token is present in the request cookies
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized("Not Refresh Token found.");
            }

            // Get the refresh token from the database
            var storeToken = _refreshTokenService.GetByToken(refreshToken);

            // Check if the refresh token is valid and not expired
            if (storeToken == null || storeToken.CurrentState == 2 || storeToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or Expires refresh token.");
            }

            // Get the user associated with the refresh token
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            // Get the claims for the user
            var newRefreshDto = new RefreshTokenDto
            {
                Tokens = newRefreshToken,
                UserId = storeToken.Id.ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CurrentState = 1,
            };
            // Generate a new access token
            _refreshTokenService.Refresh(newRefreshDto);

            // Get the claims for the user
            // var claims = await GetClaimById(storeToken.UserId);            
            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7),
            });
            return Ok(new { RefreshToken = refreshToken });
        }

        async Task<(Claim[], UserDto)> GetClaim(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };
            return (claims, user);
        }

        async Task<Claim[]> GetClaimById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };
            return claims;
        }
    }
}
