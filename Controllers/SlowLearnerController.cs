using SlowLearnerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SlowLearnerApi.Controllers
{
    public class SlowLearnerController : ApiController
    {
        SlowLearnerEntities db = new SlowLearnerEntities();
        public HttpResponseMessage GetUnApprovedDoctors()
        {
            try
            {
                var Doctors = db.Users.Where(x=>x.UserRole=="Doctor" && x.IsApproved==false).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, Doctors);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage RegisterUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
               return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
       [HttpGet]
        public HttpResponseMessage LoginUser(string Username,string Userpassword)
        {
            try
            {
                var user = db.Users.Where(x =>  x.UserPassword==Userpassword &&x.UserName==Username ).ToList();
                 return Request.CreateResponse(HttpStatusCode.OK, user);
              
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
