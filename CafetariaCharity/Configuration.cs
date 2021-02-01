using CafetariaCharity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace CafetariaCharity
{
    internal sealed class Configuration: DbMigrationsConfiguration<CafetariaCharityContext>
    {
        private bool _pendingMigrations;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            // Check if there are migrations pending to run, this can happen if database doesn't exists or if there was any
            //  change in the schema
            var migrator = new DbMigrator(this);
            _pendingMigrations = migrator.GetPendingMigrations().Any();

            // If there are pending migrations run migrator.Update() to create/update the database then run the Seed() method to populate
            //  the data if necessary
            if (_pendingMigrations)
            {
                migrator.Update();
                Seed(new CafetariaCharityContext());
            }
        }

        protected override void Seed(CafetariaCharityContext context)
        {
            Random _rand = new Random();
            var products = new List<Product>
            {
                new Product{ID = 1, Name = "Brownie", Price = 60, Stock = 48},
                new Product{ID = 2, Name = "Muffin", Price = 100, Stock = 36},
                new Product{ID = 3, Name = "Cake Pop", Price = 135, Stock = 24},
                new Product{ID = 4, Name = "Apple tart", Price = 150, Stock = 60},
                new Product{ID = 5, Name = "Water", Price = 150, Stock = 30},
                new Product{ID = 6, Name = "Shirt", Price = 200, Stock = _rand.Next(50)},
                new Product{ID = 7, Name = "Pants", Price = 300, Stock = _rand.Next(50)},
                new Product{ID = 8, Name = "Jacket", Price = 400, Stock = _rand.Next(50)},
                new Product{ID = 9, Name = "Toy", Price = 100, Stock = _rand.Next(50)}
            };
            products.ForEach(p => context.Products.AddOrUpdate(p));
            context.SaveChanges();
            base.Seed(context);
        }
    }
       
}
