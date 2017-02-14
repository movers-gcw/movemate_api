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
            Student verify = db.Students.Where(s => s.FacebookId.Equals(student.FacebookId)).FirstOrDefault<Student>();
            if (verify != null && !verify.Verified)
            {
                MailSender.SendEmail(verify.Email, verify.VerificationCode);
                return Ok();
            }
            StudentFacade facade = new StudentFacade();
            student = facade.AddVerificationCode(student);
            MailSender.SendEmail(student.Email, student.VerificationCode);
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
            Student student = db.Students.Where(s => s.FacebookId.Equals(facebookId)).FirstOrDefault<Student>();
            if (student == null || student.Verified == false)
            {
                return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult PutStudentVerification(String facebookId, String code)
        {
            Student student = db.Students.Where(s => s.FacebookId.Equals(facebookId)).FirstOrDefault<Student>();
            if (student.VerificationCode.Equals(code))
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
        public IHttpActionResult GetStudentId(String id)
        {
            var student = db.Students.Where(s => s.FacebookId.Equals(id)).FirstOrDefault<Student>();
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student.StudentId);
        }

    }
}