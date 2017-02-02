namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<movemate_api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(movemate_api.Models.ApplicationDbContext context)
        {
            context.Students.AddOrUpdate(s => s.Name,
                new Student
                {
                    Name = "Edoardo",
                    FacebookId = "10210407129686878",
                    Verified = true
                },
                new Student
                {
                    Name = "Andrea",
                    FacebookId = "0",
                    Verified = false
                }
                );
        }

    }
}
