using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapCloudGameWebAPI.Models
{
    public class UserGamesModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public required UserModel User { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public required GamesModel Game { get; set; }

        public DateTime AcquisitionDate { get; set; } = DateTime.UtcNow;
    }
}