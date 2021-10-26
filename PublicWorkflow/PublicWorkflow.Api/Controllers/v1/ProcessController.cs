using Microsoft.AspNetCore.Mvc;
using PublicWorkflow.API.Controllers;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Domain.Enum;
using System.Threading.Tasks;

namespace PublicWorkflow.Api.Controllers.v1
{
    public class ProcessController : BaseApiController<ProcessController>
    {
        [HttpGet, Route("ForMe")]
        public async Task<IActionResult> GetAll(string search, long? level, Status status, int? pageNumber, int? pageSize)
        {
            return Ok(await _mediator.Send(new GetAllProcessQuery(search, level, false, null, status, pageNumber, pageSize)));
        }

        [HttpGet, Route("Byme")]
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
        [HttpPost]
        public async Task<IActionResult> Post(CreateProcessCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        ///// <summary>
        ///// Quick Config creation
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPost,Route("QuickConfig")]
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