using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiapCloudGameWebAPI.Models
{
    public class UserModel
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Nome { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string SenhaHash { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // Relação muitos-para-muitos com jogos
        public required ICollection<GamesModel> UserGames { get; set; }
    }
}


