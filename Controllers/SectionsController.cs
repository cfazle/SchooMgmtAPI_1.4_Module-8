using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMgmtAPI.Controllers
{
    [Route("api/v1/organizations/{orgId}/users/{userId}/courses/{courseId}/sections")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SectionsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSectionsForCourses(Guid courseId)

        {
            var course = _repository.Course.GetCourses(courseId, trackChanges: false);

            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var sectionsFromDb = _repository.Section.GetSections(courseId, trackChanges: false);

            var sectionsDto = _mapper.Map<IEnumerable<SectionDto>>(sectionsFromDb);

            return Ok(sectionsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetSectionForCourse(Guid courseId, Guid id)
        {
            var course = _repository.Course.GetCourses(courseId, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }

            var sectionDb = _repository.Section.GetSection(courseId, id, trackChanges: false);
            if (sectionDb == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var section = _mapper.Map<SectionDto>(sectionDb);

            return Ok(section);
        }

        [HttpPost]
        public IActionResult CreateSectionForCourse(Guid courseId, [FromBody] SectionForCreationDto section)
        {
            if (section == null)
            {
                _logger.LogError("SectionForCreationDto object sent from client is null.");
                return BadRequest("SectionForCreationDto object is null");
            }

            //    var organization = _repository.Company.GetCompany(companyId, trackChanges: false);
            var course = _repository.Course.GetCourses(courseId, trackChanges: false);

            if (course == null)
            {
                _logger.LogInfo($"User with id: {courseId} doesn't exist in the database.");
                return NotFound();
            }


            var sectionEntity = _mapper.Map<Section>(section);

            //      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);

            _repository.Section.CreateSectionForCourse(courseId, sectionEntity);
            _repository.Save();

            var sectionToReturn = _mapper.Map<SectionDto>(sectionEntity);

            return CreatedAtRoute(new { courseId, id = sectionToReturn.Id }, sectionToReturn);
        }
    }
}
