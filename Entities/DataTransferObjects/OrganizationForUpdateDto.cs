using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
  public   class OrganizationForUpdateDto
    {
        public string OrgName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        public IEnumerable<UserForUpdateDto> users { get; set; }
    }
}
