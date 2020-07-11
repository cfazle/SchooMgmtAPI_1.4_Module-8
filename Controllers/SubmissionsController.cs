using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        public IActionResult CreateSubmissionForAssinment(Guid assignmentId, [FromBody] SubmissionForCreationDto submission)
        {
            if (submission == null)
            {
                _logger.LogError("SubmissionForCreationDto object sent from client is null.");
                return BadRequest("SubmissionForCreationDto object is null");
            }

            //    var organization = _repository.Company.GetCompany(companyId, trackChanges: false);
            var assignment = _repository.Assignment.GetAssignments(assignmentId, trackChanges: false);

            if (assignment == null)
            {
                _logger.LogInfo($"Section with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }


            var submissionEntity = _mapper.Map<Submission>(submission);

            //      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);

            _repository.Submission.CreateSubmissionForAssignment(assignmentId, submissionEntity);
            _repository.Save();

            var submissionToReturn = _mapper.Map<SubmissionDto>(submissionEntity);

            return CreatedAtRoute(new {assignmentId, id = submissionToReturn.Id }, submissionToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSubissionForAssignment(Guid assignmentId, Guid id)
        {
            var assignment = _repository.Assignment.GetAssignments(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Enrollment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionForAssignment = _repository.Submission.GetSubmission(assignmentId, id, trackChanges: false);
            if (submissionForAssignment == null)
            {
                _logger.LogInfo($"Submission with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Submission.DeleteSubmission(submissionForAssignment);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSubmissionForAssignment(Guid assignmentId, Guid id, [FromBody] SubmissionForUpdateDto submission)
        {
            if (submission == null)
            {
                _logger.LogError("SubmissionForUpdateDto object sent from client is null.");
                return BadRequest("Submission ForUpdateDto object is null");
            }

            var assignment = _repository.Assignment.GetAssignments(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionEntity = _repository.Submission.GetSubmission(assignmentId, id, trackChanges: true);
            if (submissionEntity == null)
            {
                _logger.LogInfo($"Submission with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(submission, submissionEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatesubmissionForAssignment(Guid assignmentId, Guid id, [FromBody] JsonPatchDocument<SubmissionForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var assignment = _repository.Assignment.GetAssignments(assignmentId, trackChanges: false);
            if (assignment == null)
            {
                _logger.LogInfo($"Assignment with id: {assignmentId} doesn't exist in the database.");
                return NotFound();
            }

            var submissionEntity = _repository.Submission.GetSubmission(assignmentId, id, trackChanges: true);
            if (submissionEntity == null)
            {
                _logger.LogInfo($"Submission with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var submissionToPatch = _mapper.Map<SubmissionForUpdateDto>(submissionEntity);

            patchDoc.ApplyTo(submissionToPatch);

            _mapper.Map(submissionToPatch, submissionEntity);

            _repository.Save();

            return NoContent();
        }

    }
    }
