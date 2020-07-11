using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SubmissionRepository : RepositoryBase<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(RepositoryContext repositoryContext)
             : base(repositoryContext)
        {
        }


        public Submission GetSubmission(Guid assignmentId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.AssignmentId.Equals(assignmentId) && e.Id.Equals(id), trackChanges)
                  .SingleOrDefault();

        public IEnumerable<Submission> GetSubmissions(Guid assignmentId, bool trackChanges) =>
                  FindByCondition(e => e.AssignmentId.Equals(assignmentId), trackChanges).
            OrderBy(e => e.SubmissionTitle);


        public void CreateSubmissionForAssignment(Guid assignmentId, Submission submission)
        {
            submission.AssignmentId = assignmentId;
            Create(submission);
        }

        public void DeleteSubmission(Submission submission)
        {
            Delete(submission);
        }
    }
}


