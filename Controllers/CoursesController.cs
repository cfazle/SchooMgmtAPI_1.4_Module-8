using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

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

    }
}
