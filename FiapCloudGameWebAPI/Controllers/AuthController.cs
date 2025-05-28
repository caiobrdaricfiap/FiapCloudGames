using FiapCloudGameWebAPI.Domain.Models;
using FiapCloudGames.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FiapCloudGameWebAPI.Models;
using FiapCloudGameWebAPI.Domain.Utils;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGamesWebAPI.Application.DTOs.Auth;

namespace FiapCloudGameWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        #region Construtor
        /// <summary>
        /// Construtor que injeta o repositório de usuários e as configurações.
        /// </summary>
        public AuthController(AuthService authService, UserRepository userRepository, IConfiguration configuration)
        {
            _authService = authService;
            _userRepository = userRepository;
            _configuration = configuration;
        }
        #endregion

        #region Operações

        /// <summary>
        /// Realiza o login e retorna o token JWT juntamente com informações do usuário para preenchimento de interface.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///  
        ///     {
        ///         "email": "seu@email.com",
        ///         "password": "SuaSenha123!"
        ///     }
        /// 
        /// O token JWT retornado deve ser usado no botão Authorize do topo do Swagger.
        /// </remarks>
        /// <param name="dto">Dados de login</param>
        /// <returns>Token JWT and User Data</returns>
        /// 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion
    }
}