using FiapCloudGameWebAPI.Models;
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
        private static List<GamesLibraryModel> _library = new();
        #region CRUD
        /// <summary>
        /// Retorna todos os registros da biblioteca de jogos.
        /// </summary>
        /// <returns>Lista completa da biblioteca</returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_library);

        /// <summary>
        /// Adiciona um item à biblioteca de jogos.
        /// </summary>
        /// <param name="item">Item contendo usuário, jogo e data de aquisição</param>
        /// <returns>Item adicionado com ID gerado</returns>
        [HttpPost]
        public IActionResult AddToLibrary(GamesLibraryModel item)
        {
            item.Id = _library.Count + 1;
            _library.Add(item);
            return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
        }

        /// <summary>
        /// Obtém todos os itens da biblioteca relacionados a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Lista de jogos do usuário</returns>
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var items = _library.Where(x => x.UserId == userId).ToList();
            return Ok(items);
        }

        /// <summary>
        /// Obtém todos os itens da biblioteca relacionados a um jogo específico.
        /// </summary>
        /// <param name="gameId">ID do jogo</param>
        /// <returns>Lista de usuários que possuem o jogo</returns>
        [HttpGet("game/{gameId}")]
        public IActionResult GetByGame(int gameId)
        {
            var items = _library.Where(x => x.GameId == gameId).ToList();
            return Ok(items);
        }
        #endregion
    }
}
