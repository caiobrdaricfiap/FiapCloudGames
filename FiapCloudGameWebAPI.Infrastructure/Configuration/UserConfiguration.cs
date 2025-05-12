using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiapCloudGames.Domain.Entities;
using FiapCloudGameWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("User");
            builder.Property(u => u.Id).HasColumnType("INT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(u => u.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.RegisterDate).HasColumnType("DATETIME").IsRequired();
            builder.Property(u => u.Active).HasColumnType("BIT").IsRequired();
            builder.Property(u => u.Role).HasColumnType("INT").IsRequired();
        }
    }
}