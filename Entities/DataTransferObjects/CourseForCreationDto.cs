using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class CourseForCreationDto
    {
       
        public string CourseName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public IEnumerable<SectionForCreationDto> sections { get; set; }
    }
}
