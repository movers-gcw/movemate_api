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

        // PUT: api/PointOfInterests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPointOfInterest(int id, PointOfInterest pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pointOfInterest.PointOfInterestId)
            {
                return BadRequest();
            }

            db.Entry(pointOfInterest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointOfInterestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PointOfInterests
        [ResponseType(typeof(PointOfInterest))]
        public IHttpActionResult PostPointOfInterest(PointOfInterest pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PointOfInterests.Add(pointOfInterest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pointOfInterest.PointOfInterestId }, pointOfInterest);
        }

        // DELETE: api/PointOfInterests/5
        [ResponseType(typeof(PointOfInterest))]
        public IHttpActionResult DeletePointOfInterest(int id)
        {
            PointOfInterest pointOfInterest = db.PointOfInterests.Find(id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            db.PointOfInterests.Remove(pointOfInterest);
            db.SaveChanges();

            return Ok(pointOfInterest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointOfInterestExists(int id)
        {
            return db.PointOfInterests.Count(e => e.PointOfInterestId == id) > 0;
        }
    }
}