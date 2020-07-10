using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public  class CourseDto
    {

        public Guid Id { get; set; }
        
        public string CourseName { get; set; }
     
        public DateTime CreatedDate { get; set; }

       
        public DateTime UpdatedDate { get; set; }
    }
}
