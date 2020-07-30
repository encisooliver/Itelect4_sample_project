using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace itelec4.ApiControllers
{
    public class MstCourseController : ApiController
    {
        Data.ITElec4dbDataContext db = new Data.ITElec4dbDataContext();

        [Authorize, HttpGet, Route("api/course/list")]
        public List<Api_Models.MstCourse_ApiModel> ListCourse()
        {
            var courses = from d in db.MstCourses
                          select new Api_Models.MstCourse_ApiModel
                          {
                              Id = d.Id,
                              CourseCode = d.CourseCode,
                              Course = d.Course
                          };

            return courses.ToList();
        }

        [Authorize, HttpGet, Route("api/course/list/{filter}")]
        public List<Api_Models.MstCourse_ApiModel> ListFilterCourse(String filter)
        {
            var courses = from d in db.MstCourses
                          where d.Course.ToLower().Contains(filter)
                          || d.CourseCode.ToLower().Contains(filter)
                          select new Api_Models.MstCourse_ApiModel
                          {
                              Id = d.Id,
                              CourseCode = d.CourseCode,
                              Course = d.Course
                          };

            return courses.ToList();
        }

        [Authorize, HttpGet, Route("api/course/groupdata")]
        public List<Api_Models.GroupCourseModel> ListGroupCourse()
        {
            var courses = from d in db.MstCourses
                          select new Api_Models.MstCourse_ApiModel
                          {
                              Id = d.Id,
                              CourseCode = d.CourseCode,
                              Course = d.Course
                          };

            var groupCourses = from p in courses
                               group p by p.CourseCode into g
                               select new Api_Models.GroupCourseModel
                               {
                                   Course = g.Key,
                                   Total = g.Key.Count()
                               };

            return groupCourses.ToList();
        }




        [Authorize, HttpGet, Route("api/course/filter/{filterString}")]
        public List<Api_Models.MstCourse_ApiModel> FilterGetCourse(String filterString)
        {
            var courses = from d in db.MstCourses
                          where d.Course.ToLower() == filterString.ToLower()
                          select new Api_Models.MstCourse_ApiModel
                          {
                              Id = d.Id,
                              CourseCode = d.CourseCode,
                              Course = d.Course
                          };

            return courses.ToList();
        }

        [Authorize, HttpGet, Route("api/course/detail/{id}")]
        public Api_Models.MstCourse_ApiModel DetailCourse(String id)
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

        [Authorize, HttpPost, Route("api/course/add")]
        public HttpResponseMessage AddCourse(Api_Models.MstCourse_ApiModel objCourse)
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

        [Authorize, HttpPut, Route("api/course/update/{id}")]
        public HttpResponseMessage UpdateCourse(Api_Models.MstCourse_ApiModel objCourse, String Id)
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
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Course not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize, HttpDelete, Route("api/course/delete/{id}")]
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
