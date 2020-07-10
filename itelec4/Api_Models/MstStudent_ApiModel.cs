using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itelec4.Api_Models
{
    public class MstStudent_ApiModel
    {
        public Int32 Id { get; set; }
        public String StudentCode { get; set; }
        public String FullName { get; set; }
        public Int32 CourseId { get; set; }
        public String Course { get; set; }
    }
}