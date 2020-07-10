using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }



        public IEnumerable<User> GetUsers(Guid orgId, bool trackChanges) =>
            FindByCondition(e => e.OrganizationId.Equals(orgId), trackChanges)
            .OrderBy(e => e.UserName);


        public User GetUser(Guid orgId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.OrganizationId.Equals(orgId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateUserForOrganization(Guid organizationId, User user)
        {
            user.OrganizationId = organizationId; 
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
    }
