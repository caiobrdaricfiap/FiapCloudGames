using FiapCloudGameWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Configuration
{
    public class GamesLibraryConfiguration : IEntityTypeConfiguration<GamesLibraryModel>
    {
        public void Configure(EntityTypeBuilder<GamesLibraryModel> builder)
        {
            builder.ToTable("GamesLibrary");
            builder.Property(u => u.Id).HasColumnType("INT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(u => u.UserId).HasColumnType("INT").IsRequired();
            builder.Property(u => u.GameId).HasColumnType("INT").IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Games)
                .HasPrincipalKey(u => u.Id);

            builder.HasOne(p => p.Game)
                .WithMany(u => u.Users)
                .HasPrincipalKey(u => u.Id);
        }
    }
}