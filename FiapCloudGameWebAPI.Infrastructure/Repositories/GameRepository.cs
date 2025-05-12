using FiapCloudGames.Infrastructure.Data;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Infrastructure.Repositories;
using FiapCloudGameWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiapCloudGames.Infrastructure.Repositories;

public class GameRepository : BaseRepository<GameModel>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }
}