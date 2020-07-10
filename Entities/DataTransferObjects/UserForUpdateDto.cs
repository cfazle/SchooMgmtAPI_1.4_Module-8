using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public  class UserForUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
