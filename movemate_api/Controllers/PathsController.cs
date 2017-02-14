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
    public class PathsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Path> GetPaths()
        {
            List<Path> paths = new List<Path>();
            foreach(Path p in db.Paths)
            {
                var path = new Path();
                path.Maker = p.Maker;
                path.PathId = p.PathId;
                path.PathName = p.PathName;
                paths.Add(path);
            }
            return paths.AsQueryable<Path>();
        }

        public IHttpActionResult PutJoinPath(int StudentId, int PathId)
        {
            var path = db.Paths.Find(PathId);
            var stud = db.Students.Find(StudentId);
            if(path == null || stud == null)
            {
                return BadRequest();
            }
            stud.JoinedPaths.Add(path);
            path.Participants.Add(stud);
            db.Entry(path).State = EntityState.Modified;
            db.Entry(stud).State = EntityState.Modified;
            return Ok();
        }
        public IHttpActionResult PostPath([FromBody] Pathblob blob)
        {
            var student = db.Students.Find(blob.StudentId);
            var path = new Path();
            path.PathName = blob.PathName;
            var section = new Section();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = DateTime.Parse(blob.Date);
            if(blob.ToFrom)
            {
                start.Address = blob.Address;
                destination.Address = db.Departments.Find(blob.DepId).Address;
            }
            else if(!blob.ToFrom)
            {
                destination.Address = blob.Address;
                start.Address = db.Departments.Find(blob.DepId).Address;
            }
            section.Vehicle = blob.Vehicle;
            if(section.Vehicle == 0)
            {
                section.AvailableSeats = blob.Seats;
            }
            if(section.Vehicle == 1)
            {
                section.AvailableHeadgear = blob.Head;
            }
            if(section.Vehicle == 2)
            {
                section.Description = blob.Description;
            }
            section.Train = blob.Train;
            section.Bus = blob.Bus;
            section.Tram = blob.Tram;
            section.Metro = blob.Metro;
            section.Price = blob.Price;
            section.Start = start;
            section.Destination = destination;
            start.Section = section;
            destination.Section = section;
            path.Sections.Add(section);
            section.Path = path;
            student.CreatedPaths.Add(path);
            db.Entry(student).State = EntityState.Modified;
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.Sections.Add(section);
            db.Paths.Add(path);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = path.PathId }, path);
        }

        // GET: api/Paths/5
        [ResponseType(typeof(Path))]
        public IHttpActionResult GetPath(int id)
        {
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return NotFound();
            }

            return Ok(path);
        }

        // PUT: api/Paths/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPath(int id, Path path)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != path.PathId)
            {
                return BadRequest();
            }

            db.Entry(path).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PathExists(id))
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

        // POST: api/Paths
        [ResponseType(typeof(Path))]
        public IHttpActionResult PostPathOriginal(Path path)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paths.Add(path);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = path.PathId }, path);
        }

        // DELETE: api/Paths/5
        [ResponseType(typeof(Path))]
        public IHttpActionResult DeletePath(int id)
        {
            Path path = db.Paths.Find(id);
            if (path == null)
            {
                return NotFound();
            }

            db.Paths.Remove(path);
            db.SaveChanges();

            return Ok(path);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PathExists(int id)
        {
            return db.Paths.Count(e => e.PathId == id) > 0;
        }
    }
}