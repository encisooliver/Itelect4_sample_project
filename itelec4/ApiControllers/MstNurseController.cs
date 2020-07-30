using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace itelec4.ApiControllers
{
    [Authorize, RoutePrefix("api/nurses")]
    public class MstNurseController : ApiController
    {
        Data.ITElec4dbDataContext db = new Data.ITElec4dbDataContext();

        [HttpGet, Route("list")]
        public List<Api_Models.Nurse_ApiModel> ListNurses()
        {
            var nurses = from d in db.MstNurses
                         select new Api_Models.Nurse_ApiModel
                         {
                             Id = d.Id,
                             EmployeeId = d.EmployeeId,
                             Lastname = d.Lastname,
                             FirstName = d.FirstName,
                             Position = d.Position,
                             IsRegistered = d.IsRegistered,
                             PrcNumber = d.PrcNumber,
                         };

            return nurses.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public Api_Models.Nurse_ApiModel DetailNurse(String id)
        {

            var nurses = from d in db.MstNurses
                         where d.Id == Convert.ToInt32(id)
                         select new Api_Models.Nurse_ApiModel
                         {
                             Id = d.Id,
                             EmployeeId = d.EmployeeId,
                             Lastname = d.Lastname,
                             FirstName = d.FirstName,
                             Position = d.Position,
                             IsRegistered = d.IsRegistered,
                             PrcNumber = d.PrcNumber,
                         };
            return nurses.FirstOrDefault();
        }

        //[HttpPost, Route("add")]
        //public HttpResponseMessage AddNurse(Api_Models.Nurse_ApiModel objNurse)
        //{
        //    try
        //    {
        //        Data.MstNurse newNurse = new Data.MstNurse
        //        {
        //            EmployeeId = objNurse.EmployeeId,
        //            Lastname = objNurse.Lastname,
        //            FirstName = objNurse.FirstName,
        //            Position = objNurse.Position,
        //            IsRegistered = objNurse.IsRegistered,
        //            PrcNumber = objNurse.PrcNumber,
        //        };
        //        db.MstNurses.InsertOnSubmit(newNurse);
        //        db.SubmitChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[HttpPut, Route("update/{id}")]
        //public HttpResponseMessage UpdateNurse(Api_Models.Nurse_ApiModel objNurse, String Id)
        //{
        //    try
        //    {
        //        var nurses = from d in db.MstNurses
        //                     where d.Id == Convert.ToInt32(Id)
        //                     select d;

        //        if (nurses.Any())
        //        {
        //            var updateNurse = nurses.FirstOrDefault();
        //            updateNurse.EmployeeId = objNurse.EmployeeId;
        //            updateNurse.Lastname = objNurse.Lastname;
        //            updateNurse.FirstName = objNurse.FirstName;
        //            updateNurse.Position = objNurse.Position;
        //            updateNurse.IsRegistered = objNurse.IsRegistered;
        //            updateNurse.PrcNumber = objNurse.PrcNumber;
        //            db.SubmitChanges();

        //            return Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, "Nurse not found!");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteNurse(String Id)
        {
            try
            {
                var nurse = from d in db.MstNurses
                            where d.Id == Convert.ToInt32(Id)
                            select d;

                if (nurse.Any())
                {
                    db.MstNurses.DeleteOnSubmit(nurse.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Nurse not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
