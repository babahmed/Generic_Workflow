
using PublicWorkflow.Web.Abstractions;
using PublicWorkflow.Web.Areas.Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Commands.Update;

namespace PublicWorkflow.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class OrganizationController : BaseController<OrganizationController>
    {
        public IActionResult Index()
        {
            var model = new OrganizationViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllOrganizationQuery(null,1,1000));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<OrganizationViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var brandsResponse = await _mediator.Send(new GetAllOrganizationQuery(null, 1, 1000));

            if (id == 0)
            {
                var organizationViewModel = new OrganizationViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", organizationViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetOrganizationByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var organizationViewModel = _mapper.Map<OrganizationViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", organizationViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(long id, OrganizationViewModel org)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createBrandCommand = _mapper.Map<CreateOrganizationCommand>(org);
                    var result = await _mediator.Send(createBrandCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Organization with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateOrganizationCommand = _mapper.Map<UpdateOrganizationCommand>(org);
                    var result = await _mediator.Send(updateOrganizationCommand);
                    if (result.Succeeded) _notify.Information($"Organization with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllOrganizationQuery(null, 1, 1000));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<OrganizationViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", org);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new UpdateOrganizationCommand { Id = id, IsDeleted=true });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Organization with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllOrganizationQuery(null, 1, 1000));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<OrganizationViewModel>>(response.Data);
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