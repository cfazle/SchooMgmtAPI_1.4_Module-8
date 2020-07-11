using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetSections(Guid courseId, bool trackChanges);
       Section GetSection(Guid courseId, Guid id, bool trackChanges);

        void CreateSectionForCourse(Guid courseId, Section section);
        void DeleteSection(Section section);
    }
}
