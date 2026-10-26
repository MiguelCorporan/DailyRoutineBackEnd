
//using DailyRoutine.Data;
//using DailyRoutine.DTOs;
//using DailyRoutine.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace DailyRoutine.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _configuration;

//        public AuthController(ApplicationDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        // ✅ Registro de usuario
//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterRequestDto Request)
//        {
//            // Verificar si ya existe el usuario
//            if (await _context.Users.AnyAsync(u => u.Email == Request.Email))
//                return BadRequest("El correo ya está registrado.");

//            // Encriptar la contraseña
//            using var hmac = new HMACSHA512();
//            var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(Request.Password)));

//            var user = new UserEntity
//            {
//                Username = Request.Username,
//                Email = Request.Email,
//                PasswordHash = passwordHash,
//                Role = Request.Role ?? "User",
//                Birthdate = Request.Birthdate
//            };

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "Usuario registrado con éxito" });
//        }

//        // ✅ Login y generación de JWT
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequestDto Request)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Request.Email);
//            if (user == null)
//                return Unauthorized("Usuario no encontrado.");

//            // Validar contraseña (simple por ahora)
//            var hashedInput = Convert.ToBase64String(
//                new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes(Request.Password))
//            );

//            if (hashedInput != user.PasswordHash)
//                return Unauthorized("Contraseña incorrecta.");

//            // Crear el token JWT
//            var token = CreateJwtToken(user);

//            return Ok(new
//            {
//                message = "Inicio de sesión exitoso",
//                token
//            });
//        }

//        // 🔐 Método privado para crear JWT
//        private string CreateJwtToken(UserEntity user)
//        {
//            var jwtSettings = _configuration.GetSection("Jwt");
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Role, user.Role)
//            };

//            var token = new JwtSecurityToken(
//                issuer: jwtSettings["Issuer"],
//                audience: jwtSettings["Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(2),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}

using DailyRoutine.Services;
using DailyRoutine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DailyRoutine.DTOs.LoginDTO;
using DailyRoutine.DTOs.RegisterDTO;

namespace DailyRoutine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
