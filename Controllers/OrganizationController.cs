using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using Entities.Models;
using Entities.DataTransferObjects;
using SchoolMgmtAPI.ModelBinders;

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

        [HttpGet("{id}", Name = "OrganizationById")]

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

        [HttpGet("collection/({ids})", Name = "OrganizationCollection")]
        public IActionResult GetOrganizationCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]
        IEnumerable<Guid> ids)
        //  public IActionResult GetOrganizationCollection(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var organizationEntities = _repository.Organization.GetByIds(ids, trackChanges: false);

            if (ids.Count() != organizationEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var organizationsToReturn = _mapper.Map<IEnumerable<OrganizationDto>>(organizationEntities);
            return Ok(organizationsToReturn);
        }


        [HttpPost]
        public IActionResult CreateOrganization([FromBody] OrganizationForCreationDto organization)
        {
            if (organization == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");

                return BadRequest("CompanyForCreationDto object is null");
            }

            var organizationEntity = _mapper.Map<Organization>(organization);

            _repository.Organization.CreateOrganization(organizationEntity);

            _repository.Save();

            var organizationToReturn = _mapper.Map<OrganizationDto>(organizationEntity);

            return CreatedAtRoute("OrganizationById",

                new { id = organizationToReturn.Id }, organizationToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateOrganizationCollection([FromBody]
        IEnumerable<OrganizationForCreationDto> organizationCollection)
        {
            if (organizationCollection == null)
            {
                _logger.LogError("Organization collection sent from client is null.");
                return BadRequest("Organization collection is null");
            }

            var organizationEntities = _mapper.Map<IEnumerable<Organization>>(organizationCollection);
            foreach (var organization in organizationEntities)
            {
                _repository.Organization.CreateOrganization(organization);
            }

            _repository.Save();

            var organizationCollectionToReturn = _mapper.Map<IEnumerable<OrganizationDto>>
                (organizationEntities);
            var ids = string.Join(",", organizationCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("OrganizationCollection", new { ids }, organizationCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrganization(Guid id)
        {
            var organization = _repository.Organization.GetOrganization(id, trackChanges: false);
            if (organization == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Organization.DeleteOrganization(organization);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrganization(Guid id, [FromBody] OrganizationForUpdateDto organization)
        {
            if (organization == null)
            {
                _logger.LogError("OrganizationForUpdateDto object sent from client is null.");
                return BadRequest("OrganizationForUpdateDto object is null");
            }

            var organizationEntity = _repository.Organization.GetOrganization(id, trackChanges: true);
            if (organizationEntity == null)
            {
                _logger.LogInfo($"Organization with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(organization, organizationEntity);
            _repository.Save();

            return NoContent();
        }

    }
}
