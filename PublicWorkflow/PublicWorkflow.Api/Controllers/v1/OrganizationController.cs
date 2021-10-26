using Microsoft.AspNetCore.Mvc;
using PublicWorkflow.API.Controllers;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetById;
using System.Threading.Tasks;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class OrganizationController : BaseApiController<OrganizationController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int? pageNumber, int? pageSize)
        {
            var orgs = await _mediator.Send(new GetAllOrganizationQuery(search, pageNumber, pageSize));
            return Ok(orgs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var org = await _mediator.Send(new GetOrganizationByIdQuery() { Id = id });
            return Ok(org);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrganizationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrganizationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}