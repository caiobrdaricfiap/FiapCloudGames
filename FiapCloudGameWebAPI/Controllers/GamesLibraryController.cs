using FiapCloudGamesWebAPI.Application.DTOs.GamesLibrary;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Controllers
{
    /// <summary>
    /// Controller para gerenciar a biblioteca de jogos dos usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GamesLibraryController : ControllerBase
    {
        private readonly GamesLibraryService _gamesLibraryService;

        #region Constructor

        public GamesLibraryController(GamesLibraryService gamesLibraryService)
        {
            _gamesLibraryService = gamesLibraryService;
        }

        #endregion
        #region CRUD
        /// <summary>
        /// Retorna todos os registros da biblioteca de jogos.
        /// </summary>
        /// <returns>Lista completa da biblioteca</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _gamesLibraryService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Adiciona um item à biblioteca de jogos.
        /// </summary>
        /// <param name="dto">Item contendo usuário, jogo e data de aquisição</param>
        /// <returns>Item adicionado com ID gerado</returns>
        [HttpPost]
        public async Task<IActionResult> AddToLibrary([FromBody] GamesLibraryCreateDto dto)
        {
            var result = await _gamesLibraryService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        /// <summary>
        /// Obtém todos os itens da biblioteca relacionados a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Lista de jogos do usuário</returns>
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await _gamesLibraryService.GetByUserAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os itens da biblioteca relacionados a um jogo específico.
        /// </summary>
        /// <param name="gameId">ID do jogo</param>
        /// <returns>Lista de usuários que possuem o jogo</returns>
        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetByGame(int gameId)
        {
            var result = await _gamesLibraryService.GetByGameAsync(gameId);
            return Ok(result);
        }
        #endregion
    }
}
