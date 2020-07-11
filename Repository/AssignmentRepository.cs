using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(RepositoryContext repositoryContext)
             : base(repositoryContext)
        {
        }


        public Assignment GetAssignment(Guid enrollmentId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.EnrollmentId.Equals(enrollmentId) && e.Id.Equals(id), trackChanges)
                  .SingleOrDefault();

        public IEnumerable<Assignment> GetAssignments(Guid enrollmentId, bool trackChanges) =>
                  FindByCondition(e => e.EnrollmentId.Equals(enrollmentId), trackChanges).
            OrderBy(e => e.Title);


        public void CreateAssignmentForEnrollment(Guid enrollmentId, Assignment assignment)
        {
            assignment.EnrollmentId = enrollmentId;
            Create(assignment);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            Delete(assignment);
        }
    }

       
}

