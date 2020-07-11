using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Repository
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext)
             : base(repositoryContext)
        {
        }


        public Course GetCourse(Guid userId, Guid id, bool trackChanges)=>
            FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(id), trackChanges)
                  .SingleOrDefault();
      

        public IEnumerable<Course> GetCourses(Guid userId, bool trackChanges) =>
                  FindByCondition(e => e.UserId.Equals(userId), trackChanges).
                  OrderBy(e => e.CourseName);



        public void CreateCourseForUser(Guid userId, Course course)
        {
            course.UserId = userId;
            Create(course);
        }

        public void DeleteCourse(Course course)
        {
            Delete(course);
        }
    }
}
