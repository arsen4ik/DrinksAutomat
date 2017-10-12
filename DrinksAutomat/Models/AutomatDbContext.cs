using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DrinksAutomat.Models
{
    public class AutomatDbContext: DbContext
    {
        //public DbSet<Automat> automats { get; set; }

        public DbSet<Drink> drinks { get; set; }
        public DbSet<Naminal> naminals { get; set; }
        public DbSet<Amount> amounts { get; set; }
        public DbSet<AmountPaid> amountsPaid { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Automat>().HasMany(d => d.drinks).WithOptional(o => o.automat);
        //    modelBuilder.Entity<Automat>().HasMany(d => d.naminals).WithOptional(o => o.automat);
        //    modelBuilder.Entity<Automat>().HasMany(d => d.amount).WithOptional(o => o.automat);
        //    modelBuilder.Entity<Automat>().HasMany(d => d.amountPaid).WithOptional(o => o.automat);

        //}
    }
}