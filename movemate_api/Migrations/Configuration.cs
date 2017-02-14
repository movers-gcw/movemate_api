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
            University uni = context.Universities.Find(1);
            for(int i = 1; i<2; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(2);
            for (int i = 2; i < 3; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(3);
            for (int i = 3; i < 65; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(4);
            for (int i = 65; i < 83; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(5);
            for (int i = 83; i < 95; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(6);
            for (int i = 95; i < 97; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(7);
            for (int i = 97; i < 101; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(8);
            for (int i = 101; i < 102; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(9);
            for (int i = 102; i < 106; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(10);
            for (int i = 106; i < 107; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(11);
            for (int i = 107; i < 113; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(12);
            for (int i = 113; i < 114; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(13);
            for (int i = 114; i < 115; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(14);
            for (int i = 115; i < 117; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
            uni = context.Universities.Find(15);
            for (int i = 117; i < 118; i++)
            {
                var dep = context.Departments.Find(i);
                dep.University = uni;
                uni.DepartmentList.Add(dep);
                context.Departments.AddOrUpdate(dep);
                context.Universities.AddOrUpdate(uni);
            }
            context.SaveChanges();
        }
    }
}






