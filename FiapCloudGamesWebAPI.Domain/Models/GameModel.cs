using FiapCloudGames.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiapCloudGameWebAPI.Models
{
    public class GameModel : BaseModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<GamesLibraryModel> Users { get; set; }

        public GameModel()
        {
            
        }
        public GameModel(string name, string gender, DateTime releaseDate, ICollection<GamesLibraryModel>? users = null)
        {
            Name = name;
            Gender = gender;
            ReleaseDate = releaseDate;
            Users = users;
        }
    }
}