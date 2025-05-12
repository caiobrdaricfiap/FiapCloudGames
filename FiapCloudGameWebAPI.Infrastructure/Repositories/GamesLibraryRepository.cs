using FiapCloudGames.Infrastructure.Data;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Infrastructure.Repositories;
using FiapCloudGameWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiapCloudGames.Infrastructure.Repositories;

public class GamesLibraryRepository : BaseRepository<GamesLibraryModel>, IGamesLibraryRepository
{
    public GamesLibraryRepository(ApplicationDbContext context) : base(context)
    {
    }
}