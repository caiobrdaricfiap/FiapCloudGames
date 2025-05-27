using FiapCloudGameWebAPI.Models;

namespace FiapCloudGameWebAPI.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        //Task<UserModel?> GetWithGamesAsync(int id);
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> GetDetailsByIdAsync(int id);
        Task<UserModel> AddAsync(UserModel usuario);
        Task<UserModel> UpdateAsync(UserModel usuario, int id);
        Task<UserModel?> GetByEmailAsync(string email);
        Task<bool> DeleteAsync(int id);
    }
}
