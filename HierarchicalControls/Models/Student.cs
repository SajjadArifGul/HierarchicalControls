using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HierarchicalControls.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }

        public int Age { get; set; }
    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }

        public int Age { get; set; }
        public DateTime DOB { get; set; }
    }
}