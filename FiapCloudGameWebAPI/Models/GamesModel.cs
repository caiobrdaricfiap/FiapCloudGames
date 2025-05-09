using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiapCloudGameWebAPI.Models
{
    public class GamesModel
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string GameName { get; set; }

        [Required]
        public required string GameGender { get; set; }

        public DateTime ReleaseDate { get; set; }

        // Many-to-many relationship with users
        public required ICollection<GamesModel> UserGames { get; set; }
    }
}