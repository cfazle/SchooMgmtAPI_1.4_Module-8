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

            CreateMap<OrganizationForCreationDto, Organization>();

            CreateMap<OrganizationForUpdateDto, Organization>();

            CreateMap<UserForCreationDto, User>();

            CreateMap<UserForUpdateDto, User>().ReverseMap();

            CreateMap<CourseForCreationDto, Course>();

            CreateMap<CourseForUpdateDto, Course>().ReverseMap();

            CreateMap<SectionForCreationDto, Section>();

            CreateMap<SectionForUpdateDto, Section>().ReverseMap();

            CreateMap<EnrollmentForCreationDto, Enrollment>();

            CreateMap<EnrollmentForUpdateDto, Enrollment>().ReverseMap();

            CreateMap<AssignmentForCreationDto, Assignment>();

            CreateMap<AssignmentForUpdateDto, Assignment>().ReverseMap();

            CreateMap<SubmissionForCreationDto, Submission>();

            CreateMap<SubmissionForUpdateDto, Submission>().ReverseMap();
        }
    }
}
