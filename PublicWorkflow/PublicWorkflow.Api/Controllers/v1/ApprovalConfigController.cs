using Microsoft.AspNetCore.Mvc;
using PublicWorkflow.API.Controllers;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetById;
using System.Threading.Tasks;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ApprovalConfigController : BaseApiController<ApprovalConfigController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string search, long? level, int? pageNumber, int? pageSize)
        {
            var configs = await _mediator.Send(new GetAllApprovalConfigQuery(search, level, pageNumber, pageSize));
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

        /// <summary>
        /// Create rule
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Route("Rule")]
        public async Task<IActionResult> ApprovalRule(CreateApprovalRuleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Get Approval Rule
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet, Route("Rule")]
        public async Task<IActionResult> ApprovalRule(GetAllApprovalRuleQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}