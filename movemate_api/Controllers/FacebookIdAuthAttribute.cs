using movemate_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace movemate_api.Controllers
{
    public class FacebookIdAuthAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Boolean IsUserValid(string facebookId)
        {
            Student student = db.Students.Where(s => s.FacebookId.Equals(facebookId)).FirstOrDefault<Student>();
            if (student != null && student.Verified)
            {
                return true;
            }
            return false;
        }

        private string ParseRequestHeaders(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var httpRequestHeader = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
            return httpRequestHeader;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    string facebookId = ParseRequestHeaders(actionContext);
                    if (IsUserValid(facebookId))
                    {
                        //actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
                        return;
                    }

                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }

                }
            }
            catch
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
