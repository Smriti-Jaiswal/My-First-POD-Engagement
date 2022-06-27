using WebApplication5.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity
{
    public class AccountMapping
    {
        public AccountMapping(EntityTypeBuilder<Account> entity)
        {
            entity.HasKey(t => t.AccountId);

            entity.Property(t => t.AccountNumber)
            .IsRequired();

            entity.ToTable("AccountTable");

        }
    }
}
