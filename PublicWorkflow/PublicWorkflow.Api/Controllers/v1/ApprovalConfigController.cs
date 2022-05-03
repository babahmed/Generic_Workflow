using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PublicWorkflow.API.Controllers;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetById;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Http;
using PublicWorkflow.Application.DTOs.ViewModel;
using PublicWorkflow.Domain.Enum;
using EnumsNET;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ApprovalConfigController : BaseApiController<ApprovalConfigController>
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<GetAllApprovalConfigResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(string search, long? level, int? pageNumber, int? pageSize)
        {
            var configs = await _mediator.Send(new GetAllApprovalConfigQuery(search, level, pageNumber, pageSize));
            return Ok(configs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<GetApprovalConfigByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetApprovalConfigByIdQuery() { Id = id });
            return Ok(brand);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateApprovalConfigCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApprovalRule(CreateApprovalRuleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Get Approval Rule
        /// </summary>
        /// <param name="query"></param>
        /// <returns> </returns>
        [HttpGet, Route("Rule")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<GetApprovalConfigByIdResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApprovalRule(GetAllApprovalRuleQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        
        /// <summary>
        /// gets rule type options
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("RuleTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<DropDown>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RuleTypes()
        {
            return Ok(Result<IEnumerable<DropDown>>.Success(Enum.GetValues(typeof(RuleAction)).Cast<RuleAction>().Select(c => new DropDown { Id = (int)c, Name = ((RuleAction)c).AsString(EnumFormat.Description) }), "success"));
        }

        /// <summary>
        /// get rule condition options
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("RuleConditions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<DropDown>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RuleConditions()
        {
            return Ok(Result<IEnumerable<DropDown>>.Success(Enum.GetValues(typeof(RuleAction)).Cast<RuleAction>().Select(c => new DropDown { Id = (int)c, Name = ((RuleAction)c).AsString(EnumFormat.Description) }), "success"));
        }

        /// <summary>
        /// Get rule action options
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("RuleActions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<DropDown>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RuleActions()
        {
            return Ok(Result<IEnumerable<DropDown>>.Success(Enum.GetValues(typeof(RuleAction)).Cast<RuleAction>().Select(c => new DropDown { Id = (int)c, Name = ((RuleAction)c).AsString(EnumFormat.Description) }), "success"));
        }
    }
}