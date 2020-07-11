using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext repositoryContext)
             : base(repositoryContext)
        {
        }


        public Section GetSection(Guid courseId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.CourseId.Equals(courseId) && e.Id.Equals(id), trackChanges)
                  .SingleOrDefault();


        public IEnumerable<Section> GetSections(Guid courseId, bool trackChanges) =>
                  FindByCondition(e => e.CourseId.Equals(courseId), trackChanges).
            OrderBy(e => e.Type);


        public void CreateSectionForCourse(Guid courseId, Section section)
        {
            section.CourseId = courseId;
            Create(section);
        }

        public void DeleteSection(Section section)
        {
            Delete(section);
        }
    }
}
   
