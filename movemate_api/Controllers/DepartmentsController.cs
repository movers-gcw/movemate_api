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
using Microsoft.ApplicationInsights;

namespace movemate_api.Controllers
{
    public class DepartmentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Departments/
        public IQueryable<Department> GetDepartments(int id)
        {
            List<Department> app = new List<Department>();
            foreach(Department d in db.Departments)
            {
                if(d.University.UniversityId == id)
                {
                    var dep = new Department();
                    dep.DepartmentId = d.DepartmentId;
                    dep.DepartmentName = d.DepartmentName;
                    dep.Address = d.Address;
                    app.Add(dep);
                }
            }
            var deps = app.AsQueryable<Department>();
            return deps;
        }
    }
}