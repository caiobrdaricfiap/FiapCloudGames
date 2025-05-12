using FiapCloudGames.Domain.Entities;

namespace FiapCloudGameWebAPI.Models
{
    public class GamesLibraryModel : BaseModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int GameId { get; set; }
        public GameModel Game { get; set; }

        public DateTime AcquisitionDate { get; set; } = DateTime.UtcNow;

        public GamesLibraryModel(UserModel user, GameModel game)
        {
            User = user;
            Game = game;
        }
        public GamesLibraryModel()
        {
            
        }
    }
}