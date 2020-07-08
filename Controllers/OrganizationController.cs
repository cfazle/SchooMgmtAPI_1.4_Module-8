using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.DataTransferObjects;

namespace SchoolMgmtAPI.Controllers
{
    [Route("api/v1/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public OrganizationController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
       
        public IActionResult GetOrganizations()
        {
          
                var organizations = _repository.Organization.GetAllOrganizations(trackChanges: false);
              //  return Ok(organizations);
                var organizationDto = _mapper.Map<IEnumerable<OrganizationDto>>(organizations);
               return Ok(organizationDto);
          
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganization(Guid id)
        {
            
                var organization = _repository.Organization.GetOrganization(id, trackChanges: false);
                if (organization == null)
                {
                    _logger.LogInfo($"Organization with id: {id} doesn't exist in the database.");
                    return NotFound();
                }
                else
                {
                    var organizationDto = _mapper.Map<OrganizationDto>(organization);
                    return Ok(organizationDto);
                }
            
        }
    }
}
