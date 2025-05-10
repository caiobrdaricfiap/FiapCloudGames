using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Configuration
{
    public class GamesLibraryConfiguration : IEntityTypeConfiguration<GamesLibraryEntity>
    {
        public void Configure(EntityTypeBuilder<GamesLibraryEntity> builder)
        {
            builder.ToTable("GamesLibrary");
            builder.Property(u => u.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(u => u.UserId).HasColumnType("INT").IsRequired();
            builder.Property(u => u.GameId).HasColumnType("INT").IsRequired();
            builder.HasOne(p => p.User)
                .WithMany(u => u.GamesLibrary)
                .HasForeignKey(u => u.UserId);
            builder.HasOne(p => p.Game)
                .WithMany(u => u.GamesLibrary)
                .HasForeignKey(u => u.GameId);
        }
    }
}