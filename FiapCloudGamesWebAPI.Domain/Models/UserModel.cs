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
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public bool Active { get; set; } = true;
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public ICollection<GamesLibraryModel>? Games { get; set; }

        public UserModel(){}
        public UserModel(string name, string email, string hashPassword, bool active, UserRole role, DateTime registerDate, ICollection<GamesLibraryModel>? games = null)
        {
            Name = name;
            Email = email;
            HashPassword = hashPassword;
            Active = active;
            Role = role;
            RegisterDate = registerDate;
            Games = games;
        }
    }
}


