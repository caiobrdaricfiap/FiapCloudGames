using System.Runtime.InteropServices;
using FiapCloudGames.Infrastructure.Data;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Domain.Utils;
using FiapCloudGameWebAPI.Infrastructure.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        #region Construtor
        public UserRepository(ApplicationDbContext applicationContext)
        {
            _dbContext = applicationContext;
        }

        #endregion

        #region Métodos
        public async Task<UserModel> Adicionar(UserModel usuario)
        {
            usuario.HashPassword = CriptografiaUtils.CriptografarSenha(usuario.HashPassword);

            await _dbContext.Users.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;

        }

        public async Task<List<UserModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> BuscarPorId(int id)
        {
            UserModel usuarioPorId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario com o o ID: {id} não foi encontrado");
            }

            return usuarioPorId;
        }

        public async Task<UserModel> Atualizar(UserModel usuario, int id)
        {
            UserModel usuarioPorId = await BuscarPorId(id);


            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} não foi encontrado");
            }

            usuarioPorId.Name = usuario.Name;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.HashPassword = CriptografiaUtils.CriptografarSenha(usuarioPorId.HashPassword);
            usuarioPorId.Active = usuario.Active;
            usuarioPorId.Role = usuario.Role;
            usuarioPorId.RegisterDate = usuario.RegisterDate;


            _dbContext.Users.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UserModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
                throw new Exception($"O usuario {id} não foi encontrado no banco de dados");

            _dbContext.Users.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }



        //VERIFICAR DEPOIS 
        //    public async Task<UserModel?> GetWithGamesAsync(int id) =>
        //        await _db
        //        .Include(u => u.Games)
        //            .ThenInclude(ug => ug.Game)
        //        .FirstOrDefaultAsync(u => u.Id == id);
        //}

        #endregion
    }
}