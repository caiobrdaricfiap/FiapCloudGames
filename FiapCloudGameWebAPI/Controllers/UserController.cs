using System.Runtime.CompilerServices;
using FiapCloudGames.Infrastructure.Repositories;
using FiapCloudGamesWebAPI.Application.DTOs.User;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGameWebAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        #region Construtor
        /// <summary>
        /// Construtor que injeta o service de usuários.
        /// </summary>
        /// <param name="userService">Service de usuários</param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region CRUD

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet ]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserResponseDto>>> BuscarTodosUsuarios()
        {
            var usuarios = await _userService.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Usuário encontrado</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseDto>> BuscarPorId(int id)
        {
            var usuario = await _userService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");
            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do usuário a ser cadastrado</param>
        /// <returns>Usuário cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um usuário existente pelo ID.
        /// </summary>
        /// <param name="dto">Dados atualizados do usuário</param>
        /// <param name="id">ID do usuário a ser atualizado</param>
        /// <returns>Usuário atualizado</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseDto>> AtualizarUsuario([FromBody] UserUpdateDto dto, int id)
        {
            var usuario = await _userService.UpdateAsync(id, dto);
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado para atualização.");
            return Ok(usuario);
        }

        /// <summary>
        /// Remove um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido</param>
        /// <returns>Indica se o usuário foi removido com sucesso</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var apagado = await _userService.DeleteAsync(id);
            if (!apagado)
                return NotFound($"Usuário com ID {id} não encontrado para remoção.");
            return Ok(new { apagado });
        }

        #endregion
    }
}
