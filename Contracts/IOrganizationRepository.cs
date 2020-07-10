using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Contracts
{
   public  interface IOrganizationRepository
    {
        IEnumerable<Organization> GetAllOrganizations(bool trackChanges);
        Organization GetOrganization(Guid OrganizationId, bool trackChanges);
        void CreateOrganization(Organization organization);
        IEnumerable<Organization> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteOrganization(Organization organization);
    }
}
