using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiapCloudGameWebAPI.Models
{
    public class UserModel : BaseModel
    {

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é Obrigatoria!")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter no minimo 8 caracteres!")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\|])[A-Za-z\d!@#$%^&*()_+{}\[\]:;<>,.?~\\|]+$",
           ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial! ")]
        public string HashPassword { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; } = true;
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        //public ICollection<GamesLibraryModel>? Games { get; set; }

        public UserModel() { }
        public UserModel(string name, string email, string hashPassword, bool active, UserRole role, DateTime registerDate, ICollection<GamesLibraryModel>? games = null)
        {
            Name = name;
            Email = email;
            HashPassword = hashPassword;
            Active = active;
            Role = role;
            RegisterDate = registerDate;
            //Games = games;
        }

        public void Update(string name, string email, bool active, UserRole role, string? hashPassword = null, string? salt = null)
        {
            Name = name;
            Email = email;
            Active = active;
            Role = role;

            // Só atualiza senha/salt se ambos forem informados
            if (!string.IsNullOrWhiteSpace(hashPassword) && !string.IsNullOrWhiteSpace(salt))
            {
                HashPassword = hashPassword;
                Salt = salt;
            }
        }
    }
}


