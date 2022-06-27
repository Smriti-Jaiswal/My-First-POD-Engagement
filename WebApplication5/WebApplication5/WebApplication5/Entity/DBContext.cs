using WebApplication5.Entity;
using WebApplication5.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Entity
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMapping(modelBuilder.Entity<User>());
            new AccountMapping(modelBuilder.Entity<Account>());
            new TransactionMapping(modelBuilder.Entity<Transaction>());
            new ServiceMapping(modelBuilder.Entity<Service>());
        }
    }
}