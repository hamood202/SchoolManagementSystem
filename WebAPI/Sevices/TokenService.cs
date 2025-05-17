using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace WebAPI.Sevices
{
    public class TokenService
    {
        private readonly string _securityKey;
        private readonly int AccessTokenExpiresMinutes = 15;
        private readonly int RefreshtTokenExpiryDays = 7;
        public TokenService(IConfiguration configuration)
        {
            _securityKey = configuration["JwtSetting:SecretKey"];
        }


        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            // Generate a JWT token
            //Convert the secret key to a secure format
            //Because JWT requires a key in a special format.
            //                                 Convert key to bytes
            //                                 Encryption algorithms work with bytes.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));

            //Create credentials for signing//
            //Prevent forgery - Any change to the token will invalidate the signature.
            ///                                            Robust and reliable algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


            //Create an integrated token:
            // -Claims(user data)
            //-Expiration date
            //-Signature data
            // Create the token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(AccessTokenExpiresMinutes),//تحديد وقت انتهاء الصلاحية  (استخدام UTC لتجنب مشاكل التوقيت العالمي )
                signingCredentials: credentials

            );
            //Convert Token to String 
            //Because the Token is sent as a string in the HTTP headers.
            //Enable token sending over the network
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            //Create a byte array (32 bytes = 256 bits)
            //Difficulty in guessing or duplicating the token
            var randomNumber = new byte[32];

            //Create a secure random number generator
            //Better than regular Random.
            //Higher security against attacks.

            //using  //Ensure disposal of resources after use
            using (var rng = RandomNumberGenerator.Create())
            {
                //Fill the array with secure random numbers
                rng.GetBytes(randomNumber);
            }
            //Converting bytes to a text string...
            //To facilitate the storage and transfer of tokens

            return Convert.ToBase64String(randomNumber);
        }
    }
}
