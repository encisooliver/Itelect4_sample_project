using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itelec4.Api_Models
{
    public class Nurse_ApiModel
    {
        public Int32 Id { get; set; }
        public Int32 EmployeeId { get; set; }
        public String Lastname { get; set; }
        public String FirstName { get; set; }
        public String Position { get; set; }
        public Boolean IsRegistered { get; set; }
        public Int32? PrcNumber { get; set; }
    }
}