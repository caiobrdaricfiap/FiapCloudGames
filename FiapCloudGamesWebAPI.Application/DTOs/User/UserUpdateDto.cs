using FiapCloudGames.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesWebAPI.Application.DTOs.User
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public UserRole Role { get; set; }
        public string? Password { get; set; }
    }
}
