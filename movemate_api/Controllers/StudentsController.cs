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
    public class StudentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Students
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.StudentId == id) > 0;
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult GetRegisteredStudent(String facebookId) // verifica se un utente è registrato e verificato
        {
            
            String query = String.Concat("SELECT * FROM dbo.Students WHERE FacebookId= ", facebookId);
            Student student = db.Students.SqlQuery(query).Single();
            if (student == null || student.Verified==false)
            {
                return NotFound();
            }
            return Ok();
        }
        [ResponseType(typeof(Student))]
        public Boolean GetStudentByFacebookId(String facebookId)
        {
            Student student = db.Students.SqlQuery("SELECT * FROM dbo.Students WHERE FacebookId= @p0", facebookId).Single();
            if (student != null)
            {
                return true;
            }
            return false;
        }
        [ResponseType(typeof(void))]
        public IHttpActionResult PostStudent(String facebookId, String name, String surname, String email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentFacade facade = new StudentFacade();
            Student student = facade.CreateStudent(name,surname,email,facebookId);
            db.Students.Add(student);
            db.SaveChanges();
            // da implementare invio codice email
            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
        }
    

        public IHttpActionResult PutStudentVerification(String facebookId, String code)
        {
            Student student = db.Students.SqlQuery("SELECT * FROM dbo.Students WHERE FacebookId= @p0", facebookId).Single();
            if(student.VerificationCode.Equals(code))
            {
                student.Verified = true;
                db.Entry(student).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            return StatusCode(HttpStatusCode.PreconditionFailed);
        }
    }
}