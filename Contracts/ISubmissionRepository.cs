using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISubmissionRepository
    {
        IEnumerable<Submission> GetSubmissions(Guid assignmentId, bool trackChanges);
        Submission GetSubmission(Guid assignmentId, Guid id, bool trackChanges);

        void CreateSubmissionForAssignment(Guid assignmentId, Submission submission);
        void DeleteSubmission(Submission submission);

    }
}
