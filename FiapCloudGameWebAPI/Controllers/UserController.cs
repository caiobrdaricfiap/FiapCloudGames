using System.Runtime.CompilerServices;
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
        private readonly IUserRepository _userRepository;

        #region Construtor
        /// <summary>
        /// Construtor que injeta o repositório de usuários.
        /// </summary>
        /// <param name="userRepository">Repositório de usuários</param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region CRUD

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet ]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<List<UserModel>>> BuscarTodosUsuarios()
        {
            List<UserModel> usuarios = await _userRepository.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Usuário encontrado</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserModel>> BuscarPorId(int id)
        {
            UserModel usuario = await _userRepository.BuscarPorId(id);
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");
            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="userModel">Dados do usuário a ser cadastrado</param>
        /// <returns>Usuário cadastrado</returns>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Cadastrar([FromBody] UserModel userModel)
        {
            UserModel usuario = await _userRepository.Adicionar(userModel);
            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza um usuário existente pelo ID.
        /// </summary>
        /// <param name="userModel">Dados atualizados do usuário</param>
        /// <param name="id">ID do usuário a ser atualizado</param>
        /// <returns>Usuário atualizado</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<UserModel>> AtualizarUsuario([FromBody] UserModel userModel, int id)
        {
            userModel.Id = id;
            UserModel usuario = await _userRepository.Atualizar(userModel, id);
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

        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _userRepository.Apagar(id);
            if (!apagado)
                return NotFound($"Usuário com ID {id} não encontrado para remoção.");
            return Ok(apagado);
        }

        #endregion
    }
}
