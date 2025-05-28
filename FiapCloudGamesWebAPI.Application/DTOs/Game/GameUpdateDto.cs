using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesWebAPI.Application.DTOs.Game
{
    public class GameUpdateDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
