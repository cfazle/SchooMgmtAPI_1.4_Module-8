namespace Contracts
{
    public interface IRepositoryManager
    {
        IOrganizationRepository Organization { get; }
        IUserRepository User { get; }
        IEnrollmentRepository Enrollment{ get; }
        ICourseRepository Course { get; }

        ISectionRepository Section { get;  }
        IAssignmentRepository Assignment { get; }
        ISubmissionRepository Submission { get; }

        void Save();
    }
}