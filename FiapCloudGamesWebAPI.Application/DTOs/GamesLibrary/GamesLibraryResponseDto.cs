using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesWebAPI.Application.DTOs.GamesLibrary
{
    public class GamesLibraryResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime AcquisitionDate { get; set; }
    }
}
