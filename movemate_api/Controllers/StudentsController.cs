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
        public IQueryable<StudentView> GetStudents()
        {
            var stud = new StudentView();
            var studs = new HashSet<StudentView>();
            foreach (Student s in db.Students.Include(s => s.CreatedPaths)
                                            .Include(s => s.Paths))
            {
                stud = StudentFacade.ViewFromStudent(s);
                studs.Add(stud);
            }
            return studs.AsQueryable<StudentView>();
        }

        // GET: api/Students/5
        [ResponseType(typeof(StudentSpecifiedView))]
        public IHttpActionResult GetStudent(int id)
        {
            /*Student student = db.Students.Include(s => s.CreatedPaths)
                                         .Include(s => s.Paths)
                                         .Where(s => s.StudentId == id)
                                         .FirstOrDefault<Student>();*/
            var student = from s in db.Students
                              where s.StudentId == id
                              select new
                              {
                                  StudentId = s.StudentId,
                                  Paths = (from p in db.Paths
                                           where p.MakerId == id
                                           select p).ToList()
                              };
        
            if (student == null)
            {
                return NotFound();
            }
            //var view = StudentFacade.ViewFromSpecifiedStudent(student);
            return Ok(student);
        }
        [ResponseType(typeof(StudentView))]
        public IHttpActionResult GetStudentInfo(int StudentId)
        {
            var student = db.Students.Include(s => s.Feedbacks)
                                     .Where(s => s.StudentId == StudentId).FirstOrDefault<Student>();
            if (student == null)
            {
                return NotFound();
            }
            var view = StudentFacade.ViewFromStudent(student);
            return Ok(view);
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
            student = StudentFacade.AddVerificationCode(student);
            MailSender.SendEmail(student.Email, student.VerificationCode);
            db.Students.Add(student);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = student.StudentId }, student);
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
            return Ok(student.StudentId);
        }

        public IHttpActionResult PostFeedback(int StudentId, double Rate, int LeaverId, int PathId)
        {
            var student = db.Students.Include(s => s.Feedbacks).Where(s => s.StudentId == StudentId).FirstOrDefault<Student>();
            var feedback = new Feedback();
            feedback.Rate = Rate;
            feedback.Student = student;
            feedback.StudentId = StudentId;
            student.Feedbacks.Add(feedback);
            db.Feedbacks.Add(feedback);
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            var c = new PathsController();
            return c.PutDisjoinPath(LeaverId, PathId);
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

                return Ok(student.StudentId);
            }
            return StatusCode(HttpStatusCode.PreconditionFailed);
        }
        public IHttpActionResult GetStudentId(String id)
        {
            var student = db.Students.Where(s => s.FacebookId.Equals(id)).FirstOrDefault<Student>();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.StudentId);
        }

        public IHttpActionResult PutPhotoUrl(int id, String url)
        {
            var student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            student.PhotoUrl = url;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(student);
        }

        public IHttpActionResult GetPhotoUrl(int id)
        {
            var student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.PhotoUrl);
        }

    }
}