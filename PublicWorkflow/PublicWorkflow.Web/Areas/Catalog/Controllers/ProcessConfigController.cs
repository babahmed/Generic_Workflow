using PublicWorkflow.Application.Constants;
using PublicWorkflow.Web.Abstractions;
using PublicWorkflow.Web.Areas.Catalog.Models;
using PublicWorkflow.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicWorkflow.Application.Features.Queries.GetAll;
using System;
using PublicWorkflow.Domain.Enum;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.ProcessConfigs.Commands.Update;

namespace PublicWorkflow.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class ProcessConfigController : BaseController<ProcessConfigController>
    {
        public IActionResult Index()
        {
            var model = new ProcessConfigViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllProcessConfigQuery(null,1,1,1000));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ProcessConfigViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Users.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var configResponse = await _mediator.Send(new GetAllProcessConfigQuery(null, 1, 1, 1000));

            if (id == 0)
            {
                var processConfigViewModel = new ProcessConfigViewModel();
                if (configResponse.Succeeded)
                {
                    var publishTypes = from Publish d in Enum.GetValues(typeof(Publish))
                                     select new { ID = (int)d, Name = d.ToString() };
                    processConfigViewModel.PublishType = new SelectList(publishTypes, "Id", "Name", null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", processConfigViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetProcessConfigByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var configViewModel = _mapper.Map<ProcessConfigViewModel>(response.Data);
                    if (configResponse.Succeeded)
                    {
                        var publishTypes = from Publish d in Enum.GetValues(typeof(Publish))
                                           select new { ID = (int)d, Name = d.ToString() };
                        configViewModel.PublishType = new SelectList(publishTypes, "Id", "Name", null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", configViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(long id, ProcessConfigViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createProcessCommand = _mapper.Map<CreateProcessConfigCommand>(product);
                    var result = await _mediator.Send(createProcessCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Process Configuration with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateProductCommand = _mapper.Map<UpdateProcessConfigCommand>(product);
                    var result = await _mediator.Send(updateProductCommand);
                    if (result.Succeeded) _notify.Information($"Product with ID {result.Data} Updated.");
                }
                //if (Request.Form.Files.Count > 0)
                //{
                //    IFormFile file = Request.Form.Files.FirstOrDefault();
                //    var image = file.OptimizeImageSize(700, 700);
                //    await _mediator.Send(new UpdateProductImageCommand() { Id = id, Image = image });
                //}
                var response = await _mediator.Send(new GetAllProcessConfigQuery(null, 1, 1, 1000));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ProcessConfigViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", product);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new UpdateProcessConfigCommand { Id = id , IsDeleted=true});
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Product with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllProcessConfigQuery(null, 1, 1, 1000));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ProcessConfigViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}