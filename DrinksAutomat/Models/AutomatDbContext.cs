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

    }
}