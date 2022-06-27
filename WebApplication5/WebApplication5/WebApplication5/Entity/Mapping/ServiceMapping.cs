using WebApplication5.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity
{
    public class ServiceMapping
    {
        public ServiceMapping(EntityTypeBuilder<Service> entity)
        {
            entity.HasKey(t => t.ServiceId);

            entity.Property(t => t.ReqBy)
            .IsRequired();

            entity.Property(t => t.Amount)
            .HasColumnType("decimal(10,2)");

            entity.ToTable("ServiceTable");

        }
    }
}
