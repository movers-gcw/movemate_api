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
            foreach (Path p in db.Paths)
            {
                var path = new Path();
                path.Maker = p.Maker;
                path.PathId = p.PathId;
                path.PathName = p.PathName;
                paths.Add(path);
            }
            return paths.AsQueryable<Path>();
        }

        public IQueryable<Path> GetMyPaths(int StudentId)
        {
            List<Path> paths = new List<Path>();
            Student stud = db.Students.Find(StudentId);
            foreach (Path p in db.Paths)
            {
                if (p.Maker == stud || p.Participants.Contains(stud))
                {
                    var path = new Path();
                    path.Maker = p.Maker;
                    path.PathId = p.PathId;
                    path.PathName = p.PathName;
                    paths.Add(path);
                }
            }
            return paths.AsQueryable<Path>();
        }

        public IQueryable<Student> GetParticipants(int PathId)
        {
            Path path = db.Paths.Find(PathId);
            if(path.Participants==null)
            {
                return null;
            }
            return path.Participants.AsQueryable<Student>();
        }
        public IHttpActionResult PostPathCar(int studentId, Boolean toFrom, String pathname, String date, int depId, String address, int seats, String price)
        {
            var student = db.Students.Find(studentId);
            var path = new Path();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = date;
            if (toFrom)
            {
                start.Address = address;
                destination.Address = db.Departments.Find(depId).Address;
            }
            else if (!toFrom)
            {
                destination.Address = address;
                start.Address = db.Departments.Find(depId).Address;
            }
            path.Vehicle = 0;
            path.AvailableSeats = seats;
            db.Paths.Add(path);
            start.Path = path;
            destination.Path = path;
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.SaveChanges();
            path.Start = start;
            path.Destination = destination;
            path.Price = price;
            path.PathName = pathname;
            path.Maker = student;
            db.Entry(path).State = EntityState.Modified;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PostPathCycle(int studentId, Boolean toFrom, String pathname, String date, int depId, String address, String price, Boolean head)
        {
            var student = db.Students.Find(studentId);
            var path = new Path();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = date;
            if (toFrom)
            {
                start.Address = address;
                destination.Address = db.Departments.Find(depId).Address;
            }
            else if (!toFrom)
            {
                destination.Address = address;
                start.Address = db.Departments.Find(depId).Address;
            }
            path.Vehicle = 1;
            path.AvailableHeadgear = head;
            db.Paths.Add(path);
            start.Path = path;
            destination.Path = path;
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.SaveChanges();
            path.Start = start;
            path.Destination = destination;
            path.Price = price;
            path.PathName = pathname;
            path.Maker = student;
            db.Entry(path).State = EntityState.Modified;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PostPathPub(int studentId, Boolean toFrom, String pathname, String date, int depId, String address, Boolean train, Boolean bus, Boolean metro, Boolean tram, String description)
        {
            var student = db.Students.Find(studentId);
            var path = new Path();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = date;
            if (toFrom)
            {
                start.Address = address;
                destination.Address = db.Departments.Find(depId).Address;
            }
            else if (!toFrom)
            {
                destination.Address = address;
                start.Address = db.Departments.Find(depId).Address;
            }
            path.Vehicle = 2;
            path.Description = description;
            db.Paths.Add(path);
            start.Path = path;
            destination.Path = path;
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.SaveChanges();
            path.Start = start;
            path.Destination = destination;
            path.Train = train;
            path.Bus = bus;
            path.Tram = tram;
            path.Metro = metro;
            path.PathName = pathname;
            path.Maker = student;
            db.Entry(path).State = EntityState.Modified;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult PutJoinPath(int StudentId, int PathId)
        {
            var path = db.Paths.Find(PathId);
            var stud = db.Students.Find(StudentId);
            if (path == null || stud == null)
            {
                return BadRequest();
            }
            if (stud.JoinedPaths == null)
            {
                List<Path> list = new List<Path>();
                stud.JoinedPaths.Add(path);
            }
            else
            {
                stud.JoinedPaths.Add(path);
            }
            if (path.Participants == null)
            {
                List<Student> list = new List<Student>();
                path.Participants.Add(stud);
            }
            else
            {
                path.Participants.Add(stud);
            }
            db.Entry(path).State = EntityState.Modified;
            db.Entry(stud).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PutDisjoinPath(int StudentId, int PathId)
        {
            var path = db.Paths.Find(PathId);
            var stud = db.Students.Find(StudentId);
            if (path == null || stud == null)
            {
                return BadRequest();
            }
            path.Participants.Remove(stud);
            stud.JoinedPaths.Remove(path);
            db.Entry(path).State = EntityState.Modified;
            db.Entry(stud).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult DeletePath(int StudentId, int PathId)
        {
            var path = db.Paths.Find(PathId);
            var stud = db.Students.Find(StudentId);
            if (path == null || stud == null)
            {
                return BadRequest();
            }
            if (path.Maker != stud)
            {
                return BadRequest();
            }
            db.Paths.Remove(path);
            db.SaveChanges();
            return Ok();
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