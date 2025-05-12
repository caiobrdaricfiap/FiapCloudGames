using FiapCloudGames.Infrastructure.Data;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Infrastructure.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        private readonly DbSet<UserModel> _db;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _db = context.Set<UserModel>();
        }


        public async Task<UserModel?> GetWithGamesAsync(int id) =>
            await _db
            .Include(u => u.Games)
                .ThenInclude(ug => ug.Game)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}