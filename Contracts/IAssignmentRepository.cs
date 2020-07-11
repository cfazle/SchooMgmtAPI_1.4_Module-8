using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
   public  interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAssignments(Guid enrollmentId, bool trackChanges);
        Assignment GetAssignment(Guid enrollmentId, Guid id, bool trackChanges);

        void CreateAssignmentForEnrollment(Guid enrollmentId, Assignment assignment);
        void DeleteAssignment(Assignment assignment);
    }
}
