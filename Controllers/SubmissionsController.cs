using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMgmtAPI.Controllers
{
    [Route("api/v1/organizations/{orgId}/users/{userId}/courses/{courseId}/sections/{sectionId}/enrollments" +
        "/{enrollmentId}/assignments/{assignmentId}/submissions")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        
            private readonly IRepositoryManager _repository;
            private readonly ILoggerManager _logger;
            private readonly IMapper _mapper;

            public SubmissionsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
            {
                _repository = repository;
                _logger = logger;
                _mapper = mapper;
            }

            [HttpGet]
            public IActionResult GetSubmissionsForAssignment(Guid assignmentId)

            {
                var assignment = _repository.Assignment.GetAssignments(assignmentId, trackChanges: false);

                if (assignment == null)
                {
                    _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                    return NotFound();
                }

                var submissionsFromDb = _repository.Submission.GetSubmissions(assignmentId, trackChanges: false);

                var submissionsDto = _mapper.Map<IEnumerable<SubmissionDto>>(submissionsFromDb);

                return Ok(submissionsDto);
            }
        [HttpGet("{id}")]
        public IActionResult GetSubmisionForAssignment(Guid assignmentId, Guid id)
        {
            var enrollment = _repository.Assignment.GetAssignments(assignmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionDb = _repository.Submission.GetSubmission(assignmentId, id, trackChanges: false);
            if (submissionDb == null)
            {
                _logger.LogInfo($"Submission with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var submission = _mapper.Map<SubmissionDto>(submissionDb);

            return Ok(submission);
        }

    }
    }
