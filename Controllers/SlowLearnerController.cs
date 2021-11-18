﻿using SlowLearnerApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
                var Doctors = db.Users.Where(x => x.UserRole == "Doctor" && x.IsApproved == false).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, Doctors);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetApprovedDoctors()
        {
            try
            {
                var Doctors = db.Users.Where(x => x.UserRole == "Doctor" && x.IsApproved == true).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, Doctors);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
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
        public HttpResponseMessage LoginUser(string Username, string Userpassword)
        {
            try
            {
                var user = db.Users.Where(x => x.UserPassword == Userpassword && x.UserName == Username).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage ApproveUnApproveUser(int UserId, bool b)
        {
            try
            {
                var user = db.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                if (user != null)
                {
                    user.IsApproved = b;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, user);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMyPatients(int DoctorId)
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, db.Users.Where(x => x.ReferenceUserId == DoctorId && x.UserRole == "Patient").ToList());

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetMyPA(int DoctorId)
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, db.Users.Where(x => x.ReferenceUserId == DoctorId && x.UserRole == "PA").ToList());

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage Assign_PA_To_Patient(int PA_Id, int Patient_Id)
        {
            try
            {
                var IsExist = db.PatientAttendants.FirstOrDefault(x => x.PatientAttendantId == PA_Id && x.PatientId == Patient_Id);
                if (IsExist == null)
                {
                    IsExist = new PatientAttendant();
                    IsExist.PatientId = Patient_Id;
                    IsExist.PatientAttendantId = PA_Id;
                    db.PatientAttendants.Add(IsExist);
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Assigned");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage UnAssign_PA_From_Patient(int PA_Id, int Patient_Id)
        {
            try
            {
                var IsExist = db.PatientAttendants.FirstOrDefault(x => x.PatientAttendantId == PA_Id && x.PatientId == Patient_Id);
                if (IsExist != null)
                {
                    db.PatientAttendants.Remove(IsExist);
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "UnAssigned");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

     /*   [HttpGet]
        public HttpResponseMessage Assign_Word_To_Patient(int Word_Id, int Patient_Id)
        {
            try
            {
                var IsExist = db.PatientWords.FirstOrDefault(x => x.WordId == Word_Id && x.PatientId == Patient_Id);
                if (IsExist == null)
                {
                    IsExist = new PatientWord();
                    IsExist.PatientId = Patient_Id;
                    IsExist.WordId = Word_Id;
                    db.PatientWords.Add(IsExist);
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Assigned");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage UnAssign_Word_From_Patient(int Word_Id, int Patient_Id)
        {
            try
            {
                var IsExist = db.PatientWords.FirstOrDefault(x => x.WordId == Word_Id && x.PatientId == Patient_Id);
                if (IsExist != null)
                {
                    db.PatientWords.Remove(IsExist);
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "UnAssigned");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }*/

        [HttpGet]
        public HttpResponseMessage PA_Patients(int PA_Id)
        {
            try
            {
                var patients = new List<User>();
                var patientIds = db.PatientAttendants.Where(x => x.AttendantId == PA_Id).Select(x => x.PatientId).ToList();
                if (patientIds != null)
                {
                    patients = db.Users.Where(x => patientIds.Contains(x.UserId) && x.UserRole == "Patient").ToList();

                }

                return Request.CreateResponse(HttpStatusCode.OK, patients);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage Patient_Words(int Patient_Id)
        {
            try
            {
                var practices = new List<Practice>();
                var practiceIds = db.PatientPractices.Where(x => x.PatientId == Patient_Id).Select(x => x.PracticeId).ToList();
                if (practiceIds != null)
                {
                    practices = db.Practices.Where(x => practiceIds.Contains(x.PracticeId)).ToList();

                }

                return Request.CreateResponse(HttpStatusCode.OK, practices);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetLevelPractices(int PracticeLevel)
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, db.Practices.Where(x => x.PracticeLevel == PracticeLevel).ToList());

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddNewCollection()
        {
            try
            {
                Collection collection = new Collection();

                var httpRequest = HttpContext.Current.Request;
                var keys = httpRequest.Form;
                string path = HttpContext.Current.Server.MapPath("~/Images/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    var namefile = System.Guid.NewGuid() + "_" + DateTime.Now.ToString("mmss") + System.IO.Path.GetExtension(postedFile.FileName);
                    var filePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/"), namefile);
                    collection.CollectionImage = "Images/" + namefile;
                    postedFile.SaveAs(filePath);

                }
                collection.CollectionText = keys["CollectionText"];
                collection.CollectionType = keys["CollectionType"];
              
                db.Collections.Add(collection);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
