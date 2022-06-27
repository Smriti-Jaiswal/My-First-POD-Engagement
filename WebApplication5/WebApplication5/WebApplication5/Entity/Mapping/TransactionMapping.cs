using WebApplication5.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity
{
    public class TransactionMapping
    {
        public TransactionMapping(EntityTypeBuilder<Transaction> entity)
        {
            entity.HasKey(t => t.TransactionId);

            entity.Property(t => t.Type)
            .IsRequired();

            entity.Property(t => t.Amount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

            entity.Property(t => t.CreatedOn)
            .HasColumnType("datetime");

            entity.Property(p => p.IsReqTransaction)
                .HasColumnType("bit");


            entity.HasOne(p => p.User)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.UserId);

            entity.ToTable("TransactionTable");

        }
    }
}
