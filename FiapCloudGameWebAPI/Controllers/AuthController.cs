using FiapCloudGameWebAPI.Domain.Models;
using FiapCloudGames.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FiapCloudGameWebAPI.Models;
using FiapCloudGameWebAPI.Domain.Utils;

namespace FiapCloudGameWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        #region Construtor
        /// <summary>
        /// Construtor que injeta o repositório de usuários e as configurações.
        /// </summary>
        public AuthController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        #endregion

        #region Operações

        /// <summary>
        /// Realiza o login e retorna o token JWT.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///  
        ///     {
        ///         "username": "seu@email.com",
        ///         "password": "SuaSenha123!"
        ///     }
        /// 
        /// O token JWT retornado deve ser usado no botão Authorize do topo do Swagger.
        /// </remarks>
        /// <param name="model">Dados de login</param>
        /// <returns>Token JWT</returns>
        /// 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userRepository.BuscarPorEmailAsync(model.Username);
            if (user == null)
                return Unauthorized("Usuário ou senha inválidos.");

            // Gere o hash da senha informada usando o salt salvo
            string hashTentativa = CriptografiaUtils.HashPassWord(model.Password, user.Salt);
            if (user.HashPassword != hashTentativa)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        /// <summary>
        /// Gera o token JWT para o usuário autenticado.
        /// </summary>
        private string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("A chave JWT não está configurada no appsettings.json.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)); var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}