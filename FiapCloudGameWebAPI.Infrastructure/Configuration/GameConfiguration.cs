
using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.ToTable("Game");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(u => u.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Description).HasColumnType("VARCHAR(100)");
            builder.Property(u => u.Gender).HasColumnType("VARCHAR(20)").IsRequired();
            builder.Property(u => u.Image).HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(u => u.Price).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.ReleaseYear).HasColumnType("VARCHAR(4)").IsRequired();
        }
    }
}