//namespace DailyRoutine.Services
//{
//    public class AuthService
//    {
//    }
//}


using DailyRoutine.Data;
using DailyRoutine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DailyRoutine.Interfaces;
using BCrypt.Net;
using DailyRoutine.DTOs.LoginDTO;
using DailyRoutine.DTOs.RegisterDTO;

namespace DailyRoutine.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        //{
        //    var response = new RegisterResponseDto();

        //    if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        //    {
        //        response.Success = false;
        //        response.Message = "El correo ya está registrado.";
        //        return response;
        //    }

        //    using var hmac = new HMACSHA512();
        //    var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        //    var user = new UserEntity
        //    {
        //        Username = request.Username,
        //        Email = request.Email,
        //        PasswordHash = passwordHash,
        //        Role = request.Role ?? "User",
        //        Birthdate = request.Birthdate
        //    };

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    response.Success = true;
        //    response.Message = "Usuario registrado correctamente.";
        //    response.UserId = user.Id;

        //    return response;
        //}

        //public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        //{
        //    var response = new LoginResponseDto();

        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        //    if (user == null)
        //    {
        //        response.Success = false;
        //        response.Message = "Usuario no encontrado.";
        //        return response;
        //    }

        //    var inputHash = Convert.ToBase64String(
        //        new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes(request.Password))
        //    );

        //    if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        //    {
        //        response.Success = false;
        //        response.Message = "Contraseña incorrecta.";
        //        return response;
        //    }

        //    var jwtToken = CreateJwtToken(user);

        //    response.Success = true;
        //    response.Message = "Inicio de sesión exitoso.";
        //    response.Token = jwtToken;

        //    return response;
        //}

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var res = new RegisterResponseDto();
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                res.Success = false; res.Message = "El correo ya está registrado."; return res;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new UserEntity
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role ?? "User",
                Birthdate = request.Birthdate
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            res.Success = true; res.Message = "Usuario registrado correctamente."; res.UserId = user.Id;
            return res;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var res = new LoginResponseDto();
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) { res.Success = false; res.Message = "Usuario no encontrado."; return res; }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                res.Success = false; res.Message = "Contraseña incorrecta."; return res;
            }

            var token = CreateJwtToken(user);
            res.Success = true; res.Message = "Login exitoso."; res.Token = token;
            return res;
        }



        // 🔐 Crear token JWT
        private string CreateJwtToken(UserEntity user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
