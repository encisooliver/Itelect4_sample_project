using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace itelec4.ApiControllers
{
    [RoutePrefix("api/course")]
    public class MstCourseController : ApiController
    {
        Data.ITElec4dbDataContext db = new Data.ITElec4dbDataContext();

        [HttpGet, Route("list")]
        public List<Api_Models.MstCourse_ApiModel> GetCourses()
        {

            var courses = from d in db.MstCourses
                          select new Api_Models.MstCourse_ApiModel
                          {
                              Id = d.Id,
                              CourseCode = d.CourseCode,
                              Course = d.Course
                          };

            return courses.OrderByDescending(d => d.CourseCode).ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public Api_Models.MstCourse_ApiModel GetCourse(String id)
        {

            var courses = from d in db.MstCourses
                          where d.Id == Convert.ToInt32(id)
                          select new Api_Models.MstCourse_ApiModel
                          {
                              Id = d.Id,
                              CourseCode = d.CourseCode,
                              Course = d.Course
                          };

            return courses.FirstOrDefault();
        }

        [HttpPost, Route("course/add")]
        public HttpResponseMessage AddCourse(Api_Models.MstCourse_ApiModel objCourse, String Id)
        {
            try
            {
                Data.MstCourse newCourse = new Data.MstCourse
                {
                    CourseCode = objCourse.CourseCode,
                    Course = objCourse.Course
                };
                db.MstCourses.InsertOnSubmit(newCourse);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("course/update/{id}")]
        public HttpResponseMessage SaveCourse(Api_Models.MstCourse_ApiModel objCourse, String Id)
        {
            try
            {
                var course = from d in db.MstCourses
                             where d.Id == Convert.ToInt32(Id)
                             select d;

                if (course.Any())
                {
                    var updateCourse = course.FirstOrDefault();
                    updateCourse.CourseCode = objCourse.CourseCode;
                    updateCourse.Course = objCourse.Course;

                    db.MstCourses.InsertOnSubmit(updateCourse);
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Course not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("course/delete/{id}")]
        public HttpResponseMessage DeleteCourse(String Id)
        {
            try
            {
                var course = from d in db.MstCourses
                             where d.Id == Convert.ToInt32(Id)
                             select d;

                if (course.Any())
                {
                    db.MstCourses.DeleteOnSubmit(course.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Course not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
