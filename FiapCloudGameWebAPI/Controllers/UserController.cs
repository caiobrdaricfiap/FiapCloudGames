using System.Runtime.CompilerServices;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGameWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        #region Construtor
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region CRUD

        [HttpGet]
        public async Task<ActionResult <List<UserModel>>> BuscarTodosUsuarios()
        {
            List<UserModel> usuarios = await _userRepository.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult <UserModel>> BuscarPorId(int id)
        {
            UserModel usuario = await _userRepository.BuscarPorId(id);
            return Ok(usuario);

        }

        [HttpPost]
        public async Task<ActionResult <UserModel>> Cadastrar([FromBody] UserModel userModel)
        {
            UserModel usuario = await _userRepository.Adicionar(userModel);
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<ActionResult <UserModel>> AtualizarUsuario([FromBody] UserModel userModel, int id)
        {
            userModel.Id = id;
            UserModel usuario = await _userRepository.Atualizar(userModel, id);
            return Ok(usuario);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult <UserModel>> Apagar(int id)
        {
            bool apagado = await _userRepository.Apagar(id);

            return Ok(apagado);
        }

        #endregion


    }
}
