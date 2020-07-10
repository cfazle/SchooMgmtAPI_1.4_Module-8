using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IOrganizationRepository _organizationRepository;
        private IUserRepository _userRepository;
        private ICourseRepository _courseRepository;
        private ISectionRepository _sectionRepository;
        private IEnrollmentRepository _enrollmentRepository;
        private IAssignmentRepository _assignmentRepository;
        private ISubmissionRepository _submissionRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IOrganizationRepository Organization
        {
            get
            {
                if (_organizationRepository == null)
                    _organizationRepository = new OrganizationRepository(_repositoryContext);

                return _organizationRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if (_courseRepository == null)
                    _courseRepository = new CourseRepository(_repositoryContext);

                return _courseRepository;
            }
        }

        public ISectionRepository Section
        {
            get
            {
                if (_sectionRepository == null)
                    _sectionRepository = new SectionRepository(_repositoryContext);

                return _sectionRepository;
            }
        }

        public IEnrollmentRepository Enrollment
        {
            get
            {
                if (_enrollmentRepository == null)
                    _enrollmentRepository = new EnrollmentRepository(_repositoryContext);

                return _enrollmentRepository;
            }
        }

      

        public ISubmissionRepository Submission
        {
            get
            {
                if (_submissionRepository == null)
                    _submissionRepository = new SubmissionRepository(_repositoryContext);

                return _submissionRepository;
            }
        }

        public IAssignmentRepository Assignment
        {
            get
            {
                if (_assignmentRepository == null)
                    _assignmentRepository = new AssignmentRepository(_repositoryContext);

                return _assignmentRepository;
            }
        }

                public void Save() => _repositoryContext.SaveChanges();
    }
}
