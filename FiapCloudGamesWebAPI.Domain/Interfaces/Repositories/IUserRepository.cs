using FiapCloudGameWebAPI.Models;

namespace FiapCloudGameWebAPI.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        //Task<UserModel?> GetWithGamesAsync(int id);
        Task<List<UserModel>> BuscarTodosUsuarios();
        Task<UserModel> BuscarPorId(int id);
        Task<UserModel> Adicionar(UserModel usuario);
        Task<UserModel> Atualizar(UserModel usuario, int id);
        Task<UserModel?> BuscarPorEmailAsync(string email);
        Task<bool> Apagar(int id);
    }
}
