using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.Property(u => u.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(u => u.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.DateRegister).HasColumnType("DATETIME").IsRequired();
            builder.Property(u => u.Active).HasColumnType("BIT").IsRequired();
            builder.Property(u => u.Role).HasColumnType("INT").IsRequired();
        }
    }
}