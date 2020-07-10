using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public  class UserForCreationDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

       public IEnumerable<CourseForCreationDto> courses { get; set; }
    }
}
