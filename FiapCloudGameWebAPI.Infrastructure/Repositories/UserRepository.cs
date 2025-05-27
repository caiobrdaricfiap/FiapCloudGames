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
        public async Task<UserModel> AddAsync(UserModel usuario)
        {
            await _dbContext.Users.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetDetailsByIdAsync(int id)
        {
            UserModel usuarioPorId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario com o o ID: {id} não foi encontrado");
            }

            return usuarioPorId;
        }

        public async Task<UserModel> UpdateAsync(UserModel usuario, int id)
        {
            var usuarioPorId = await GetDetailsByIdAsync(id);

            if (usuarioPorId == null)
                throw new Exception($"Usuário para o ID: {id} não foi encontrado");

            _dbContext.Users.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuarioPorId = await GetDetailsByIdAsync(id);
            if (usuarioPorId == null)
                return false;

            _dbContext.Users.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
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