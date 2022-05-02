using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PublicWorkflow.API.Controllers;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.ProcessConfigs.Commands.Update;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetById;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Http;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ProcessConfigController : BaseApiController<ProcessConfigController>
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(string search, long? orgId, int? pageNumber, int? pageSize)
        {
            var configs = await _mediator.Send(new GetAllProcessConfigQuery(search, orgId, pageNumber, pageSize));
            return Ok(configs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetOrganizationByIdQuery() { Id = id });
            return Ok(brand);
        }

        // POST api/<controller>
        [HttpPost, Route("Config")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateProcessConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Quick Config creation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Route("QuickConfig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateQuickProcessConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(UpdateProcessConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Quick Config creation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Route("Rule")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApprovalRule(CreateProcessRuleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Get Approval Rule
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet, Route("Rule")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<GetAllOrganizationResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetApprovalRules(GetAllProcessRuleQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}