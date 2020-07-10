﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMgmtAPI.Controllers
{
    [Route("api/v1/organizations/{orgId}/users/{userId}/courses/{courseId}/sections/{sectionId}/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EnrollmentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEnrollmentsForSection(Guid sectionId)

        {
            var section = _repository.Section.GetSections(sectionId, trackChanges: false);

            if (section == null)
            {
                _logger.LogInfo($"Section with id: {sectionId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentsFromDb = _repository.Enrollment.GetEnrollments(sectionId, trackChanges: false);

            var enrollmentsDto = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentsFromDb);

            return Ok(enrollmentsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetEnrollmentForSection(Guid sectionId, Guid id)
        {
            var organization = _repository.Section.GetSections(sectionId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"Section with id: {sectionId} doesn't exist in the database.");
                return NotFound();
            }

            var enrollmentDb = _repository.Enrollment.GetEnrollment(sectionId, id, trackChanges: false);
            if (enrollmentDb == null)
            {
                _logger.LogInfo($"Enrollment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var enrollment = _mapper.Map<EnrollmentDto>(enrollmentDb);

            return Ok(enrollment);
        }

        [HttpPost]
        public IActionResult CreateEnrollmentForSection(Guid sectionId, [FromBody] EnrollmentForCreationDto enrollment)
        {
            if (enrollment == null)
            {
                _logger.LogError("EnrollmentForCreationDto object sent from client is null.");
                return BadRequest("EnrollmentForCreationDto object is null");
            }

            //    var organization = _repository.Company.GetCompany(companyId, trackChanges: false);
            var section = _repository.Section.GetSections(sectionId, trackChanges: false);

            if (section == null)
            {
                _logger.LogInfo($"Section with id: {sectionId} doesn't exist in the database.");
                return NotFound();
            }


            var enrollmentEntity = _mapper.Map<Enrollment>(enrollment);

            //      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);

            _repository.Enrollment.CreateEnrollmentForSection(sectionId, enrollmentEntity);
            _repository.Save();

            var enrollmentToReturn = _mapper.Map<EnrollmentDto>(enrollmentEntity);

            return CreatedAtRoute(new { sectionId, id = enrollmentToReturn.Id }, enrollmentToReturn);
        }
    }
}
