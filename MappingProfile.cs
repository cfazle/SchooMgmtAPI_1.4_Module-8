using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolMgmtAPI
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() 
        { 
            CreateMap<Organization, OrganizationDto>().ForMember
                (c => c.FullAddress, opt => opt.MapFrom(x => string.Join(",", x.Address, x.Country)));

            CreateMap<User, UserDto>();

            CreateMap<Course, CourseDto>();

            CreateMap<Section, SectionDto>();

            CreateMap<Enrollment, EnrollmentDto>();

            CreateMap<Assignment, AssignmentDto>();

            CreateMap<Submission, SubmissionDto>();
        }
    }
}
