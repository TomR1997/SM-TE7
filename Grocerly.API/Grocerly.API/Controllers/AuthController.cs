using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Grocerly.API.Utils;
using Grocerly.Database;
using Grocerly.Database.Pocos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Grocerly.API.Controllers
{
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly GrocerlyContext _context;

        public AuthController(GrocerlyContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {
            var hashed = PasswordHasher.HashPassword(password);
            Users user = _context.Users.SingleOrDefault(u => u.Username.Equals(username) && u.Password.Equals(hashed));

            if (user == null)
                throw new UnauthorizedAccessException("Invalid username or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("kaaskaaskaaskaaskaaskaas");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("name", user.Name),
                    new Claim("id", user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Request.HttpContext.Response.Headers.Add("Authorization", tokenHandler.WriteToken(token));

            return Ok(user);
        }

        
    }

}
