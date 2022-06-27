using WebApplication5.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity
{
    public class UserMapping
    {
        public UserMapping(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(t => t.UserId);

            entity.Property(t => t.Email)
            .IsRequired();

            entity.Property(t => t.Password)
            .IsRequired();

            entity.Property(t => t.CreatedOn)
            .HasColumnType("datetime");

            entity.Property(t => t.DateOfBirth)
                .IsRequired()
           .HasColumnType("datetime");

            entity.Property(p => p.IsDeleted)
                .HasColumnType("bit");


            entity.ToTable("UserTable");

        }
    }
}
