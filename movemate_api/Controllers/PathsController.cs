﻿using System;
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
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace movemate_api.Controllers
{
    public class PathsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<PathView> GetPaths()
        {
            var paths = new HashSet<PathView>();
            var path = new PathView();
            foreach (Path p in db.Paths.Include(p => p.Start)
                                       .Include(p => p.Destination))
            {
                path = PathFacade.ViewFromPath(p);
                paths.Add(path);
            }
            return paths.AsQueryable<PathView>();
        }

        public IQueryable<PathView> GetMyPaths(int StudentId)
        {
            var me = db.Students.Find(StudentId);
            var created = db.Paths.Include(p => p.Start)
                                  .Include(p => p.Destination)
                                  .Include(p => p.Students)
                                  .Where(p => p.MakerId == StudentId).ToList<Path>();
            var joined = db.Paths.Include(p => p.Start)
                                 .Include(p => p.Destination)
                                 .Include(p => p.Students).ToList<Path>();
            var result = new HashSet<PathView>();
            foreach(Path p in joined)
            {
                if(p.Students.Contains(me))
                {
                    var view = PathFacade.ViewFromPath(p);
                    result.Add(view);
                }
            }
            foreach(Path p in created)
            {
                var path = PathFacade.ViewFromPath(p);
                result.Add(path);
            }
            return result.AsQueryable<PathView>();
        }

        public IHttpActionResult GetPath(int PathId)
        {
            var path = new PathSpecifiedView();

            Path app = db.Paths.Include(p => p.Start)
                               .Include(p => p.Destination)
                               .Include(p => p.Students)
                               .Where(p => p.PathId == PathId).FirstOrDefault();
            path = PathFacade.ViewFromPathSpecified(app);
            Student m = db.Students.Find(app.MakerId);
            var view = StudentFacade.ViewFromStudent(m);
            path.Maker = view;
            if (app.Students != null)
            {
                path.Participants = StudentFacade.ViewFromParticipants(app.Students);
            }
            return Ok(path);

        }
        public IQueryable<Student> GetParticipants(int PathId)
        {
            Path path = db.Paths.Include(p => p.Students)
                                .Where(p => p.PathId == PathId)
                                .FirstOrDefault<Path>();
            if (path.Students == null)
            {
                return null;
            }
            return path.Students.AsQueryable<Student>();
        }
        public IHttpActionResult PostPathCar(PathCarBlob blob)
        {
            var student = db.Students.Find(blob.StudentId);
            var path = new Path();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = blob.Date;
            if (blob.ToFrom)
            {
                start.Address = blob.Address;
                destination.Address = db.Departments.Find(blob.DepId).Address;
            }
            else if (!blob.ToFrom)
            {
                destination.Address = blob.Address;
                start.Address = db.Departments.Find(blob.DepId).Address;
            }
            path.Price = blob.Price;
            path.PathName = blob.PathName;
            path.Vehicle = 0;
            path.AvailableSeats = blob.Seats;
            start.Path = path;
            destination.Path = path;
            path.Maker = student;
            db.Paths.Add(path);
            db.SaveChanges();
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.SaveChanges();
            path.Start = start;
            path.Destination = destination;
            db.Entry(path).State = EntityState.Modified;
            if (student.CreatedPaths == null)
            {
                student.CreatedPaths = new HashSet<Path>();
                student.CreatedPaths.Add(path);
            }
            else
            {
                student.CreatedPaths.Add(path);
            }
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PostPathCyc(PathCycBlob blob)
        {
            var student = db.Students.Find(blob.StudentId);
            var path = new Path();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = blob.Date;
            if (blob.ToFrom)
            {
                start.Address = blob.Address;
                destination.Address = db.Departments.Find(blob.DepId).Address;
            }
            else if (!blob.ToFrom)
            {
                destination.Address = blob.Address;
                start.Address = db.Departments.Find(blob.DepId).Address;
            }
            path.Price = blob.Price;
            path.PathName = blob.PathName;
            path.Vehicle = 1;
            path.AvailableHeadgear = blob.Head;
            start.Path = path;
            destination.Path = path;
            path.Maker = student;
            db.Paths.Add(path);
            db.SaveChanges();
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.SaveChanges();
            path.Start = start;
            path.Destination = destination;
            db.Entry(path).State = EntityState.Modified;
            if (student.CreatedPaths == null)
            {
                student.CreatedPaths = new HashSet<Path>();
                student.CreatedPaths.Add(path);
            }
            else
            {
                student.CreatedPaths.Add(path);
            }
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PostPathPub(PathPubBlob blob)
        {
            var student = db.Students.Find(blob.StudentId);
            var path = new Path();
            var start = new PointOfInterest();
            var destination = new PointOfInterest();
            start.DateTime = blob.Date;
            if (blob.ToFrom)
            {
                start.Address = blob.Address;
                destination.Address = db.Departments.Find(blob.DepId).Address;
            }
            else if (!blob.ToFrom)
            {
                destination.Address = blob.Address;
                start.Address = db.Departments.Find(blob.DepId).Address;
            }
            path.Train = blob.Train;
            path.Bus = blob.Bus;
            path.Metro = blob.Metro;
            path.Tram = blob.Tram;
            path.Description = blob.Description;
            path.PathName = blob.PathName;
            path.Vehicle = 2;
            path.Price = "0";
            start.Path = path;
            destination.Path = path;
            path.Maker = student;
            db.Paths.Add(path);
            db.SaveChanges();
            db.PointOfInterests.Add(start);
            db.PointOfInterests.Add(destination);
            db.SaveChanges();
            path.Start = start;
            path.Destination = destination;
            db.Entry(path).State = EntityState.Modified;
            if (student.CreatedPaths == null)
            {
                student.CreatedPaths = new HashSet<Path>();
                student.CreatedPaths.Add(path);
            }
            else
            {
                student.CreatedPaths.Add(path);
            }
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult PutJoinPath(int StudentId, int PathId)
        {
            var path = db.Paths.Include(p => p.Students)
                               .Include(p => p.Maker)
                               .Where(p => p.PathId == PathId)
                               .FirstOrDefault<Path>(); ;
            var stud = db.Students.Include(s => s.Paths)
                                  .Where(s => s.StudentId == StudentId)
                                  .FirstOrDefault<Student>();
            if (path == null || stud == null)
            {
                return BadRequest();
            }
            if(path.MakerId == stud.StudentId)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            path.Students.Add(stud);
            db.Entry(path).State = EntityState.Modified;
            db.SaveChanges();
            stud.Paths.Add(path);
            db.Entry(stud).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PutDisjoinPath(int StudentId, int PathId)
        {
            var path = db.Paths.Include(p => p.Students)
                               .Include(p => p.Maker)
                               .Where(p => p.PathId == PathId)
                               .FirstOrDefault<Path>(); ;
            var stud = db.Students.Include(s => s.Paths)
                                  .Where(s => s.StudentId == StudentId)
                                  .FirstOrDefault<Student>();
            if (path == null || stud == null)
            {
                return BadRequest();
            }
            path.Students.Remove(stud);
            stud.Paths.Remove(path);
            db.Entry(stud).State = EntityState.Modified;
            db.Entry(path).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult DeletePath(int StudentId, int PathId)
        {
            var path = db.Paths.Include(p => p.Students)
                               .Where(p => p.PathId == PathId)
                               .FirstOrDefault<Path>();
            var stud = db.Students.Find(StudentId);
            if (path == null || stud == null)
            {
                return BadRequest();
            }
            if (path.Maker != stud)
            {
                return BadRequest();
            }
            stud.CreatedPaths.Remove(path);
            if (path.Students != null)
            {
                foreach (Student s in path.Students)
                {
                    s.Paths.Remove(path);
                }
            }
            db.Paths.Remove(path);
            db.SaveChanges();
            return Ok();
        }
        // GET: api/Paths/5
        [ResponseType(typeof(Path))]

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