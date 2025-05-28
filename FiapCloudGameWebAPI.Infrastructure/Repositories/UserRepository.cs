using System.Runtime.InteropServices;
using FiapCloudGames.Infrastructure.Data;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Domain.Utils;
using FiapCloudGameWebAPI.Infrastructure.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        #region Métodos

        public async Task<UserModel?> GetByEmailAsync(string email)
        {
            return await _context.Set<UserModel>().FirstOrDefaultAsync(u => u.Email == email);
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