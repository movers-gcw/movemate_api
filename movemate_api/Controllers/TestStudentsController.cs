using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Description;
using movemate_api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace movemate_api.Controllers
{
    [TestClass]
    public class TestStudentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [TestMethod]
        public void TestMethod1()
        {
            StudentsController controller = new StudentsController();
            String id = "10210407129686878";
            IHttpActionResult result = controller.FindRegisteredStudent(id);
            Assert.AreEqual<IHttpActionResult>(result, Ok());
        }

        public void TestMethod2()
        {
            IQueryable<String> test = from s in db.Students
                                      where s.Name.Equals("Andrea")
                                      select s.Name;
            Assert.AreEqual(test, "Andrea");
        }
    }
}