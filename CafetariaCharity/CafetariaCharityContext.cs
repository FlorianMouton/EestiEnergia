using CafetariaCharity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using CafetariaCharity;


namespace CafetariaCharity
{
    public class CafetariaCharityContext : DbContext
    {
        public CafetariaCharityContext() : base("CafetariaCharityDB")
        {

        }

        public static CafetariaCharityContext Create()
        {
            return new CafetariaCharityContext();
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
