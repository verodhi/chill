using Chill.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Repository
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>().HasKey(c => c.AccountNumber);
            builder.Entity<Account>().Property(c => c.Balance).IsRequired();
            builder.Entity<Account>().Property(c => c.Currency).IsRequired();
            builder.Entity<Account>().Property(c => c.RowVersion)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
