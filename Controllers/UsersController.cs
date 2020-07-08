using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
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
    }
}
