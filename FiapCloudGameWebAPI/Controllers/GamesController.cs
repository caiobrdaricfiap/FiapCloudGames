using FiapCloudGamesWebAPI.Application.DTOs.Game;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento dos jogos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GameService _gameService;

        #region Construtor
        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }
        #endregion

        #region CRUD

        /// <summary>
        /// Retorna todos os jogos cadastrados.
        /// </summary>
        /// <returns>Lista de jogos</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        /// <summary>
        /// Retorna um jogo pelo seu Id.
        /// </summary>
        /// <param name="id">Id do jogo</param>
        /// <returns>Jogo correspondente ou NotFound</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
                return NotFound("Jogo não encontrado.");
            return Ok(game);
        }

        /// <summary>
        /// Cria um novo jogo.
        /// </summary>
        /// <param name="dto">Objeto com dados do jogo</param>
        /// <returns>O jogo criado com o novo Id</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] GameCreateDto dto)
        {
            var created = await _gameService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um jogo existente pelo Id.
        /// </summary>
        /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="dto">Dados atualizados do jogo</param>
        /// <returns>Status da operação</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] GameUpdateDto dto)
        {
            var updated = await _gameService.UpdateAsync(id, dto);
            if (!updated) return NotFound("Jogo não encontrado.");
            return NoContent();
        }

        /// <summary>
        /// Remove um jogo pelo Id.
        /// </summary>
        /// <param name="id">Id do jogo a ser removido</param>
        /// <returns>Status da operação</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _gameService.DeleteAsync(id);
            if (!deleted) return NotFound("Jogo não encontrado.");
            return NoContent();
        }

        #endregion
    }
}
