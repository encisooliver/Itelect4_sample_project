using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace itelec4.ApiControllers
{
    [Authorize, RoutePrefix("api/doctor")]
    public class MstDoctorController : ApiController
    {
        Data.ITElec4dbDataContext db = new Data.ITElec4dbDataContext();

        [HttpGet, Route("list")]
        public List<Api_Models.Doctor_ApiModel> ListDoctors()
        {
            var doctors = from d in db.MstDoctors
                          select new Api_Models.Doctor_ApiModel
                          {
                              Id = d.Id,
                              EmployeeId = d.EmployeeId,
                              Lastname = d.Lastname,
                              FirstName = d.FirstName,
                              Department = d.Department,
                              Position = d.Position,
                              PrcNumber = d.PrcNumber,
                          };

            return doctors.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public Api_Models.Doctor_ApiModel DetailDoctor(String id)
        {

            var doctors = from d in db.MstDoctors
                          where d.Id == Convert.ToInt32(id)
                          select new Api_Models.Doctor_ApiModel
                          {
                              Id = d.Id,
                              EmployeeId = d.EmployeeId,
                              Lastname = d.Lastname,
                              FirstName = d.FirstName,
                              Department = d.Department,
                              Position = d.Position,
                              PrcNumber = d.PrcNumber,
                          };

            return doctors.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddDoctpr(Api_Models.Doctor_ApiModel objDoctor)
        {
            try
            {
                Data.MstDoctor newDoctor = new Data.MstDoctor
                {
                    EmployeeId = objDoctor.EmployeeId,
                    Lastname = objDoctor.Lastname,
                    FirstName = objDoctor.FirstName,
                    Department = objDoctor.Department,
                    Position = objDoctor.Position,
                    PrcNumber = objDoctor.PrcNumber,
                };
                db.MstDoctors.InsertOnSubmit(newDoctor);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateDoctor(Api_Models.Doctor_ApiModel objDoctor, String Id)
        {
            try
            {
                var doctor = from d in db.MstDoctors
                             where d.Id == Convert.ToInt32(Id)
                             select d;

                if (doctor.Any())
                {
                    var updateDoctor = doctor.FirstOrDefault();
                    updateDoctor.EmployeeId = objDoctor.EmployeeId;
                    updateDoctor.Lastname = objDoctor.Lastname;
                    updateDoctor.FirstName = objDoctor.FirstName;
                    updateDoctor.Department = objDoctor.Department;
                    updateDoctor.Position = objDoctor.Position;
                    updateDoctor.PrcNumber = objDoctor.PrcNumber;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Doctor not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteDoctor(String Id)
        {
            try
            {
                var doctors = from d in db.MstDoctors
                              where d.Id == Convert.ToInt32(Id)
                              select d;

                if (doctors.Any())
                {
                    db.MstDoctors.DeleteOnSubmit(doctors.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Doctor not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
