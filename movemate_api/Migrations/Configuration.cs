namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using System.Reflection;
    using System.IO;
    using CsvHelper;
    using System.Text;
    using EntityFramework.Seeder;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<movemate_api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(movemate_api.Models.ApplicationDbContext context)
        {
            
        }
    }
}






