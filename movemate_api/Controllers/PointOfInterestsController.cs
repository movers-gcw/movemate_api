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
    public class PointOfInterestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PointOfInterests
        public IQueryable<PointOfInterest> GetPointOfInterests()
        {
            return db.PointOfInterests;
        }

        // GET: api/PointOfInterests/5
        [ResponseType(typeof(PointOfInterest))]
        public IHttpActionResult GetPointOfInterest(int id)
        {
            PointOfInterest pointOfInterest = db.PointOfInterests.Find(id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        private bool PointOfInterestExists(int id)
        {
            return db.PointOfInterests.Count(e => e.PointOfInterestId == id) > 0;
        }
    }
}