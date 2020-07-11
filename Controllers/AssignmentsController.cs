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
        "/{enrollmentId}/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AssignmentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAssignmentsForEnrollment(Guid enrollmentId)

        {
            var enrollment = _repository.Enrollment.GetEnrollments(enrollmentId, trackChanges: false);

            if (enrollment == null)
            {
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentsFromDb = _repository.Assignment.GetAssignments(enrollmentId, trackChanges: false);

            var assignmentsDto = _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsFromDb);

            return Ok(assignmentsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetAssinmentForSection(Guid enrollmentId, Guid id)
        {
            var enrollment = _repository.Enrollment.GetEnrollments(enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentDb = _repository.Assignment.GetAssignment(enrollmentId, id, trackChanges: false);
            if (assignmentDb == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignment = _mapper.Map<AssignmentDto>(assignmentDb);

            return Ok(assignment);
        }

        [HttpPost]
        public IActionResult CreateAssinmentForEnrollment(Guid enrollmentId, [FromBody] AssignmentForCreationDto assignment)
        {
            if (assignment == null)
            {
                _logger.LogError("AssignmentForCreationDto object sent from client is null.");
                return BadRequest("AssignmentForCreationDto object is null");
            }

            //    var organization = _repository.Company.GetCompany(companyId, trackChanges: false);
            var enrollment = _repository.Enrollment.GetEnrollments(enrollmentId, trackChanges: false);

            if (enrollment == null)
            {
                _logger.LogInfo($"Section with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }


            var assignmentEntity = _mapper.Map<Assignment>(assignment);

            //      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);

            _repository.Assignment.CreateAssignmentForEnrollment(enrollmentId, assignmentEntity);
            _repository.Save();

            var assignmentToReturn = _mapper.Map<AssignmentDto>(assignmentEntity);

            return CreatedAtRoute(new { enrollmentId, id = assignmentToReturn.Id }, assignmentToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAssignmenttForEnrollment(Guid enrollmentId, Guid id)
        {
            var enrollment = _repository.Enrollment.GetEnrollments(enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentForEnrollment = _repository.Assignment.GetAssignment(enrollmentId, id, trackChanges: false);
            if (assignmentForEnrollment == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Assignment.DeleteAssignment(assignmentForEnrollment);
            _repository.Save();

            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateAssignmentForEnrollment(Guid enrollmentId, Guid id, [FromBody] AssignmentForUpdateDto assignment)
        {
            if (assignment == null)
            {
                _logger.LogError("AssignmentForUpdateDto object sent from client is null.");
                return BadRequest("AssignmentForUpdateDto object is null");
            }

            var enrollment = _repository.Enrollment.GetEnrollments(enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = _repository.Assignment.GetAssignment(enrollmentId, id, trackChanges: true);
            if (assignmentEntity == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(assignment, assignmentEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateAssignmentForEnrollment(Guid enrollmentId, Guid id, [FromBody] JsonPatchDocument<AssignmentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var enrollment = _repository.Enrollment.GetEnrollments(enrollmentId, trackChanges: false);
            if (enrollment == null)
            {
                _logger.LogInfo($"Enrollment with id: {enrollmentId} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentEntity = _repository.Enrollment.GetEnrollment(enrollmentId, id, trackChanges: true);
            if (assignmentEntity == null)
            {
                _logger.LogInfo($"Assignment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var assignmentToPatch = _mapper.Map<AssignmentForUpdateDto>(assignmentEntity);

            patchDoc.ApplyTo(assignmentToPatch);

            _mapper.Map(assignmentToPatch, assignmentEntity);

            _repository.Save();

            return NoContent();
        }
    }
}
