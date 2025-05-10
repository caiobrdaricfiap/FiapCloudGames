using FiapCloudGames.Domain.Entities;
using FiapCloudGameWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(){}
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserModel> Games { get; set; }
        public DbSet<UserGamesModel> GamesLibrary { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
              optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}