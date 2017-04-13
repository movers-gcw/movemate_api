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
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace movemate_api.Controllers
{
    public class StudentsController : ApiController
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        [FacebookIdAuth]
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
        [FacebookIdAuth]
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

        [FacebookIdAuth]
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
        [Anonymous]
        public async Task<HttpResponseMessage> PostStudent(StudentBlob blob)
        {
            Student student = new Models.Student();
            student.Name = blob.Name;
            student.Surname = blob.Surname;
            student.FacebookId = blob.FacebookId;
            student.Email = blob.Email;
            student.PhoneNumber = blob.PhoneNumber;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state non valid");
            }
            Student verify = db.Students.Where(s => s.Email.Equals(student.Email)).FirstOrDefault<Student>();
            if(verify!=null && !verify.FacebookId.Equals(student.FacebookId))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "email già in uso");
            }
            verify = db.Students.Where(s => s.FacebookId.Equals(student.FacebookId)).FirstOrDefault<Student>();
            if (verify != null && !verify.Verified)
            {
                if(verify.Email != null)
                {
                    verify.Email = student.Email;
                    db.Entry(verify).State = EntityState.Modified;
                    db.SaveChanges();
                }
                MailSender.SendEmail(verify.Email, verify.VerificationCode);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else if (verify != null && verify.Verified)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                student = StudentFacade.AddVerificationCode(student);
                MailSender.SendEmail(student.Email, student.VerificationCode);
                db.Students.Add(student);
                db.SaveChanges();
                string uri = blob.PhotoUri;
                return await PutStdImageByLink(student.FacebookId, uri);
            }    
        }
        [FacebookIdAuth]
        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.StudentId == id) > 0;
        }

        [Anonymous]
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

        [FacebookIdAuth]
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
            return PutDisjoinPath(LeaverId, PathId);
        }

        [FacebookIdAuth]
        private IHttpActionResult PutDisjoinPath(int StudentId, int PathId)
        {
            var path = db.Paths.Include(p => p.Students)
                               .Include(p => p.Maker)
                               .Where(p => p.PathId == PathId)
                               .FirstOrDefault<Models.Path>(); ;
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

        [Anonymous]
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

        [Anonymous]
        public IHttpActionResult GetStudentId(String id)
        {
            var student = db.Students.Where(s => s.FacebookId.Equals(id)).FirstOrDefault<Student>();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.StudentId);
        }

        [Anonymous]
        [ResponseType(typeof(HttpStatusCode))]
        public async Task<HttpResponseMessage> PutStdImageByLink(string id, string uri)
        {
            //Create http request to get photo by uri
            HttpClient sender = new HttpClient()
            {
                MaxResponseContentBufferSize = 1280 * 1024
            };

            byte[] photoBuffer;
            string decoded = uri.Replace(',', '&'); // risostituisce le occorrenze di , inserito al posto di & su android
                                                    // per evitare passaggi di parametri inesistenti
            try
            {
                photoBuffer = await sender.GetByteArrayAsync(decoded);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            sender.Dispose();

            //Create image object to represent photo
            Image photo;
            using (Stream photoStream = new MemoryStream(photoBuffer.Length))
            {
                await photoStream.WriteAsync(photoBuffer, 0, photoBuffer.Length);

                try
                {
                    photo = Image.FromStream(photoStream);
                }
                catch (ArgumentException e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
                }
            }
            photo.Dispose();

            //Find Student by his Id
            Student student = db.Students.Where(s => s.FacebookId.Equals(id)).FirstOrDefault<Student>();
            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student not found");
            }

            //Convert photo to base64 format and save it into the student's "Photo" item (string)
            student.PhotoBase = Convert.ToBase64String(photoBuffer);

            db.Entry(student).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbe)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, dbe.Message);
            }
            photoBuffer = null;
            student = null;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [FacebookIdAuth]
        public IHttpActionResult GetPhoto(int id)
        {
            Student student = db.Students.Find(id);
            if(student==null)
            {
                return NotFound();
            }
            return Ok(student.PhotoBase);
        }

        [FacebookIdAuth]
        public HttpResponseMessage GetImage(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if(student.PhotoBase==null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            byte[] buffer = Convert.FromBase64String(student.PhotoBase);
            MemoryStream stream = new MemoryStream(buffer);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return response;
        }

      /*public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return Ok();
        }*/

    }
}