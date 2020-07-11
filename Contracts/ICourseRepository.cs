using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses(Guid userId, bool trackChanges);
        Course GetCourse(Guid userId, Guid id, bool trackChanges);

        void CreateCourseForUser(Guid userId,Course course);
        void DeleteCourse(Course course);
    }
    
}
