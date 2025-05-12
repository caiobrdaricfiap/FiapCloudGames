using FiapCloudGameWebAPI.Models;

namespace FiapCloudGameWebAPI.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel?> GetWithGamesAsync(int id);
    }
}
