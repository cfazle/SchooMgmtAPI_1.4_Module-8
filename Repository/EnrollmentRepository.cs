using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }


        public Enrollment GetEnrollment(Guid sectionId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.SectionId.Equals(sectionId) && e.Id.Equals(id), trackChanges)
                  .SingleOrDefault();


        public IEnumerable<Enrollment> GetEnrollments(Guid sectionId, bool trackChanges) =>
                  FindByCondition(e => e.SectionId.Equals(sectionId), trackChanges).
            OrderBy(e => e.AttributeName);


        public void CreateEnrollmentForSection(Guid sectionId, Enrollment enrollment)
        {
            enrollment.SectionId = sectionId;
            Create(enrollment);
        }

        public void DeleteEnrollment(Enrollment enrollment)
        {
           Delete(enrollment);
        }
    }
}
   

