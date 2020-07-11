using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace SchoolMgmtAPI.Controllers
{
    [Route("api/v1/organizations/{orgId}/users/{userId}/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CoursesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCoursesForUser(Guid userId)

        {
            var user = _repository.User.GetUsers(userId, trackChanges: false);

            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var coursesFromDb = _repository.Course.GetCourses(userId, trackChanges: false);

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(coursesFromDb);

            return Ok(coursesDto);
        }

       //    [HttpGet("{id}", Name = "GetCourseForUser")]

       [HttpGet("{id}")]
        public IActionResult GetCourseForUser(Guid userId, Guid id)
        {
            var organization = _repository.User.GetUsers(userId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var courseDb = _repository.Course.GetCourse(userId, id, trackChanges: false);
            if (courseDb == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var course = _mapper.Map<CourseDto>(courseDb);

            return Ok(course);
        }

        [HttpPost]
        public IActionResult CreateCourseForUser( Guid userId, [FromBody] CourseForCreationDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForCreationDto object sent from client is null.");
                return BadRequest("CourseForCreationDto object is null");
            }

            //    var organization = _repository.Company.GetCompany(companyId, trackChanges: false);
            var user = _repository.User.GetUsers( userId,  trackChanges: false);

            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(course);

            //      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);

            _repository.Course.CreateCourseForUser(userId, courseEntity);
            _repository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute( new { userId, id = courseToReturn.Id }, courseToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourseForUser(Guid userId, Guid id)
        {
            var user = _repository.User.GetUsers(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var courseForUser = _repository.Course.GetCourse(userId, id, trackChanges: false);
            if (courseForUser == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Course.DeleteCourse(courseForUser);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourseForUser(Guid userId, Guid id, [FromBody] CourseForUpdateDto course)
        {
            if (course == null)
            {
                _logger.LogError("CourseForUpdateDto object sent from client is null.");
                return BadRequest("CourseForUpdateDto object is null");
            }

            var user = _repository.User.GetUsers(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var courseEntity = _repository.Course.GetCourse(userId, id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(course, courseEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateCourseForUser(Guid userId, Guid id, [FromBody] JsonPatchDocument<CourseForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var user = _repository.User.GetUsers(userId, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {userId} doesn't exist in the database.");
                return NotFound();
            }

            var courseEntity = _repository.Course.GetCourse(userId, id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseEntity);

            patchDoc.ApplyTo(courseToPatch);

            _mapper.Map(courseToPatch, courseEntity);

            _repository.Save();

            return NoContent();
        }


    }
}
