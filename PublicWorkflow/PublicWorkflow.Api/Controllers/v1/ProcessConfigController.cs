using PublicWorkflow.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.ProcessConfigs.Commands.Update;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetById;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ProcessConfigController : BaseApiController<ProcessConfigController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string search,long? orgId, int? pageNumber, int? pageSize)
        {
            var configs = await _mediator.Send(new GetAllProcessConfigQuery( search, orgId, pageNumber, pageSize));
            return Ok(configs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetOrganizationByIdQuery() { Id = id });
            return Ok(brand);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProcessConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProcessConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}