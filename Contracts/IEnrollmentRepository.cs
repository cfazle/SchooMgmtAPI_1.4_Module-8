using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEnrollmentRepository
    {
        IEnumerable<Enrollment> GetEnrollments(Guid sectionId, bool trackChanges);
        Enrollment GetEnrollment(Guid sectionId, Guid id, bool trackChanges);

        void CreateEnrollmentForSection(Guid sectionId, Enrollment Enrollment);
        void DeleteEnrollment(Enrollment enrollment);
    }
}
