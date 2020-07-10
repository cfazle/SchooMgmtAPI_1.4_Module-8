using System;
using System.Collections.Generic;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolMgmtAPI.Controllers
{
    [Route("api/v1/organizations/{orgId}/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsersForOrganization(Guid orgId)

        {
            var organization = _repository.Organization.GetOrganization(orgId, trackChanges: false);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {orgId} doesn't exist in the database.");
                return NotFound();
            }

            var usersFromDb = _repository.User.GetUsers(orgId, trackChanges: false);

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersFromDb);

            return Ok(usersDto);
        }

         [HttpGet("{id}", Name = "GetUserForOrganization")]
       // [HttpGet("{id}")]
        public IActionResult GetUserForOrganization(Guid orgId, Guid id)
        {
            var organization = _repository.Organization.GetOrganization(orgId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"User with id: {orgId} doesn't exist in the database.");
                return NotFound();
            }

            var userDb = _repository.User.GetUser(orgId, id, trackChanges: false);
            if (userDb == null)
            {
                _logger.LogInfo($"user with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(userDb);

            return Ok(user);
        }


        [HttpPost]
        public IActionResult CreateUserForOrganization(Guid orgId, [FromBody] UserForCreationDto user)
        {
            if (user== null)
            {
                _logger.LogError("UserForCreationDto object sent from client is null.");
                return BadRequest("UserForCreationDto object is null");
            }

            //    var organization = _repository.Company.GetCompany(companyId, trackChanges: false);
            var organization = _repository.Organization.GetOrganization(orgId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {orgId} doesn't exist in the database.");
                return NotFound();
            }

            var userEntity = _mapper.Map<User>(user);

            //      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);

            _repository.User.CreateUserForOrganization(orgId, userEntity);
            _repository.Save();

            var userToReturn = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("GetUserForOrganization", new { orgId, id = userToReturn.Id }, userToReturn);
         //   return CreatedAtRoute( new { orgId, id = userToReturn.Id }, userToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserorOrganization(Guid orgId, Guid id)
        {
            var organization = _repository.Organization.GetOrganization(orgId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {orgId} doesn't exist in the database.");
                return NotFound();
            }

            var userForOrganization = _repository.User.GetUser(orgId, id, trackChanges: false);
            if (userForOrganization == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.User.DeleteUser(userForOrganization);
            _repository.Save();

            return NoContent();
        }
       
        
        [HttpPut("{id}")]
        public IActionResult UpdateUserForOrganization(Guid orgId, Guid id, [FromBody] UserForUpdateDto user)
        {
            if (user == null)
            {
                _logger.LogError("UserForUpdateDto object sent from client is null.");
                return BadRequest("UserForUpdateDto object is null");
            }

            var organization = _repository.Organization.GetOrganization(orgId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {orgId} doesn't exist in the database.");
                return NotFound();
            }

            var userEntity = _repository.User.GetUser(orgId, id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(user, userEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateUserForOrganization(Guid orgId, Guid id, [FromBody] JsonPatchDocument<UserForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var organization = _repository.Organization.GetOrganization(orgId, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"Company with id: {orgId} doesn't exist in the database.");
                return NotFound();
            }

            var userEntity = _repository.User.GetUser(orgId, id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var userToPatch = _mapper.Map<UserForUpdateDto>(userEntity);

            patchDoc.ApplyTo(userToPatch);

            _mapper.Map(userToPatch, userEntity);

            _repository.Save();

            return NoContent();
        }
    }
}
    

