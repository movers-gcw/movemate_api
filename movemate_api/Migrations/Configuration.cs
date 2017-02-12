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

    internal sealed class Configuration : DbMigrationsConfiguration<movemate_api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(movemate_api.Models.ApplicationDbContext context)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "movemate_api.Migrations.universities.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var uni = csvReader.GetRecords<University>().ToArray();
                    context.Universities.AddOrUpdate(u => u.UniversityName, uni);
                }
            }
            resourceName = "movemate_api.Migrations.departments.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    while (csvReader.Read())
                    {
                        var department = csvReader.GetRecord<Department>();
                        var uniName = csvReader.GetField<String>("UniversityName");
                        department.University = context.Universities.Local.Single(u => u.UniversityName.Equals(uniName));
                        context.Departments.AddOrUpdate(d => d.DepartmentId, department);
                    }
                }
            }
            resourceName = "movemate_api.Migrations.pois.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    while (csvReader.Read())
                    {
                        var poi = csvReader.GetRecord<PointOfInterest>();
                        var depName = csvReader.GetField<String>("DepartmentName");
                        poi.Department = context.Departments.Local.Single(d => d.DepartmentName == depName);
                        context.PointsOfInterest.AddOrUpdate(p => p.PoiId, poi);
                    }
                }
            }
        }
    }
}        
                        /*context.Students.AddOrUpdate(s => s.StudentId,
                        new Student
                        {
                            Name = "Edoardo",
                            FacebookId = "10210407129686878",
                            Verified = true
                        }
                        );*/

                        /*Student student = context.Students.Where(s => s.StudentId == 118).FirstOrDefault<Student>();
                        student.Verified = true;
                        context.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();*/

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


                    

    

