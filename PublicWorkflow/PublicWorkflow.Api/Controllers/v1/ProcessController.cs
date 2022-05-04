using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PublicWorkflow.API.Controllers;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Domain.Enum;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using EnumsNET;
using Microsoft.AspNetCore.Http;
using PublicWorkflow.Application.DTOs.ViewModel;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Domain.Entities.Catalog;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ProcessController : BaseApiController<ProcessController>
    {
        [HttpGet, Route("ForMe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<ProcessView>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(string search, long? level, Status status, int? pageNumber, int? pageSize)
        {
            return Ok(await _mediator.Send(new GetAllProcessQuery(search, level, false, null, status, pageNumber, pageSize)));
        }

        [HttpGet, Route("Byme")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<ProcessView>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMyProcess(string search, long? level, int? processId, Status status, int? pageNumber, int? pageSize)
        {
            return Ok(await _mediator.Send(new GetAllProcessQuery(search, level, true, processId, status, pageNumber, pageSize)));
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var brand = await _mediator.Send(new GetOrganizationByIdQuery() { Id = id });
        //    return Ok(brand);
        //}

        // POST api/<controller>
        [HttpPost, Route("New")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateProcessCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // POST api/<controller>
        [HttpPost, Route("Treat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(UpdateLevelCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet, Route("Status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<DropDown>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RuleActions()
        {
            return Ok(Result<IEnumerable<DropDown>>.Success(Enum.GetValues(typeof(Status)).Cast<Status>().Select(c => new DropDown { Id = (int)c, Name = ((Status)c).AsString(EnumFormat.Description) }), "success"));
        }

        /// <summary>
        /// Quick Config creation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpPost, Route("QuickConfig")]
        //public async Task<IActionResult> Post(CreateQuickProcessConfigCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}

        //// PUT api/<controller>/5
        //[HttpPut]
        //public async Task<IActionResult> Put(UpdateProcessConfigCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}
    }
}