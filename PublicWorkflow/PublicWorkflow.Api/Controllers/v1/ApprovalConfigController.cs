using PublicWorkflow.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.Features.Commands.Update;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ApprovalConfigController : BaseApiController<ApprovalConfigController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string search,long? level, int? pageNumber, int? pageSize)
        {
            var configs = await _mediator.Send(new GetAllApprovalConfigQuery( search, level, pageNumber, pageSize));
            return Ok(configs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetApprovalConfigByIdQuery() { Id = id });
            return Ok(brand);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateApprovalConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateApprovalConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}