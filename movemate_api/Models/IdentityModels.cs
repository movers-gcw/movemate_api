﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace movemate_api.Models
{
    // È possibile aggiungere dati di profilo dell'utente specificando altre proprietà della classe ApplicationUser. Per ulteriori informazioni, visitare http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Tenere presente che il valore di authenticationType deve corrispondere a quello definito in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Aggiungere qui i reclami utente personalizzati
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<movemate_api.Models.Student> Students { get; set; }
        public System.Data.Entity.DbSet<movemate_api.Models.University> Universities { get; set; }
        public System.Data.Entity.DbSet<movemate_api.Models.Department> Departments { get; set; }
        public System.Data.Entity.DbSet<movemate_api.Models.PointOfInterest> PointOfInterests { get; set; }
        public System.Data.Entity.DbSet<movemate_api.Models.Path> Paths { get; set; }
        public System.Data.Entity.DbSet<movemate_api.Models.Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

           modelBuilder.Entity<Student>()
                        .HasMany<Path>(s => s.Paths)
                        .WithMany(p => p.Students)
                        .Map(x =>
                        {
                            x.MapLeftKey("StudentId");
                            x.MapRightKey("PathId");
                            x.ToTable("StudentJoinedPaths");
                        });
                        
            base.OnModelCreating(modelBuilder);
        }
    }
}