using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiapCloudGameWebAPI.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string UserEmail { get; set; }

        [Required]
        public required string UserHashPassword { get; set; }

        public DateTime UserRegisterDate { get; set; } = DateTime.UtcNow;

        // Relação muitos-para-muitos com jogos
        public required ICollection<GamesModel> UserGames { get; set; }
    }
}


