using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi1.Data;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;

        private readonly AppSetting _appSettings;

        public UserController(MyDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.NguoiDungs
                .SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user == null) //Không đúng 
            {
                return Ok( new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username/password"
                });
            }
            //Tạo token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Login successful",
                Data = GenerateToken(user),
            });
        }
        private string GenerateToken(NguoiDung user)
        {
            // Tạo token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.HoTen.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserName",user.UserName),
                    new Claim("Id", user.Id.ToString()),

                    // roles
                    new Claim("TokenId" , Guid.NewGuid().ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
