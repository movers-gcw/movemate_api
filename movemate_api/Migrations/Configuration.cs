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
            /*context.Students.AddOrUpdate(s => s.StudentId,
                new Student
                {
                    Name = "Edoardo",
                    FacebookId = "10210407129686878",
                    Verified = true
                }
                );*/

            Student student = context.Students.Where(s => s.StudentId == 118).FirstOrDefault<Student>();
            student.Verified = true;
            context.Entry(student).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

            // comandi per cancellare tutti i dati da tutte le tabelle
            /*for(int i = 1; i < 1000; i++)
            {
                
                Student student = context.Students.Find(i);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
            }*/


        }

    }
}
