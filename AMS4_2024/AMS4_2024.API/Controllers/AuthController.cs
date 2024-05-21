using AMS4_2024.Application.DTOs;
using AMS4_2024.Application.Services;
using AMS4_2024.Infra.Data.Repositories;
using AMS4_2024.Domain.Interfaces;
using AMS4_2024.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace AMS4_2024.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly RegisterUsuario _registerUsuario;

        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(RegisterUsuario registerUsuario, IUsuarioRepository usuarioRepository)
        {
            _registerUsuario = registerUsuario;
            _usuarioRepository = usuarioRepository;
            _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _usuarioRepository.UsuarioExistenteAsync(request.Email))
            {
                return BadRequest(new {Message = "Email ja cadastrado"});
            }
            var userDto = new UserDto
            {
                Nome = request.Nome,
                Senha = request.Senha,
                Email = request.Email
            };

            await _registerUsuario.ExecuteAsyncs(userDto);
            return Ok(new { Message = "Usuario registrado com sucesso" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmail(loginRequest.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Senha, usuario.Senha))
            {
                return Unauthorized(new { Message = "Credenciais Invalidas" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveUser(string email)
        {
            var del = await _usuarioRepository.RemoveUsuarioByEmail(email);
            if (!del)
            {
                return BadRequest(new { Message = "Usuario nao encontrado" });
            }
            return Ok(new { Message = "Usuario deletado" });
        }

        [HttpGet]
        public async Task<ActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAll();

            if (usuarios != null)
            {
                return Ok(usuarios);
            }
            else
            {
                return NotFound(new { Message = "Nenhum usuario registrado" });
            }
        }
    }

}
