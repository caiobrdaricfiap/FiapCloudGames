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
        public required string Nome { get; set; }

        [Required]
        public required string Genero { get; set; }

        public DateTime DataLancamento { get; set; }

        // Relação muitos-para-muitos com usuários
        public required ICollection<GamesModel> UserGames { get; set; }
    }
}