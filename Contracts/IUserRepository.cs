using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(Guid orgId, bool trackChanges);
        User GetUser(Guid organizationId, Guid id, bool trackChanges);
        void CreateUserForOrganization(Guid organizationId, User user);
        void DeleteUser(User user);

    }
   
}
