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
        private static List<GameModel> _games = new();

        #region CRUD
        /// <summary>
        /// Retorna todos os jogos cadastrados.
        /// </summary>
        /// <returns>Lista de jogos</returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_games);

        /// <summary>
        /// Retorna um jogo pelo seu Id.
        /// </summary>
        /// <param name="id">Id do jogo</param>
        /// <returns>Jogo correspondente ou NotFound</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult GetById(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            return game == null ? NotFound("Jogo não encontrado.") : Ok(game);
        }

        /// <summary>
        /// Cria um novo jogo.
        /// </summary>
        /// <param name="game">Objeto com dados do jogo</param>
        /// <returns>O jogo criado com o novo Id</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(GameModel game)
        {
            game.Id = _games.Count + 1;
            _games.Add(game);
            return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
        }

        /// <summary>
        /// Atualiza um jogo existente pelo Id.
        /// </summary>
        /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="updated">Dados atualizados do jogo</param>
        /// <returns>Status da operação</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Update(int id, GameModel updated)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game == null) return NotFound("Jogo não encontrado.");

            game.Name = updated.Name;
            game.Gender = updated.Gender;
            return NoContent();
        }

        /// <summary>
        /// Remove um jogo pelo Id.
        /// </summary>
        /// <param name="id">Id do jogo a ser removido</param>
        /// <returns>Status da operação</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game == null) return NotFound("Jogo não encontrado.");

            _games.Remove(game);
            return NoContent();
        }
        #endregion
    }
}
