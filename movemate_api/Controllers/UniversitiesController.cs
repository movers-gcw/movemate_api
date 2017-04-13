using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using movemate_api.Models;

namespace movemate_api.Controllers
{
    [FacebookIdAuth]
    public class UniversitiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Universities
        public IQueryable<University> GetUniversities()
        {
            List<University> app = new List<University>();
            foreach(University u in db.Universities)
            {
                University uni = new University();
                uni.UniversityId = u.UniversityId;
                uni.UniversityName = u.UniversityName;
                app.Add(uni);
            }
            var unis = app.AsQueryable<University>();
            return unis;
        }
    }
}