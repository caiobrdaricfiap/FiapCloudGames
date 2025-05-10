using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapCloudGameWebAPI.Models
{
    public class UserGamesModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public required UserModel Usuario { get; set; }

        [ForeignKey("Jogo")]
        public int GameId { get; set; }
        public required GamesModel Jogo { get; set; }

        public DateTime DataAquisicao { get; set; } = DateTime.UtcNow;
    }
}