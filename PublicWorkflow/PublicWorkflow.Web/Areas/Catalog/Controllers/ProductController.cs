﻿using PublicWorkflow.Application.Constants;
using PublicWorkflow.Application.Features.Brands.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Products.Commands.Create;
using PublicWorkflow.Application.Features.Products.Commands.Delete;
using PublicWorkflow.Application.Features.Products.Commands.Update;
using PublicWorkflow.Application.Features.Products.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Products.Queries.GetById;
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

namespace PublicWorkflow.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class ProductController : BaseController<ProductController>
    {
        public IActionResult Index()
        {
            var model = new ProductViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllProductsCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Users.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var brandsResponse = await _mediator.Send(new GetAllBrandsCachedQuery());

            if (id == 0)
            {
                var productViewModel = new ProductViewModel();
                if (brandsResponse.Succeeded)
                {
                    var brandViewModel = _mapper.Map<List<BrandViewModel>>(brandsResponse.Data);
                    productViewModel.Brands = new SelectList(brandViewModel, nameof(BrandViewModel.Id), nameof(BrandViewModel.Name), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", productViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetProductByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var productViewModel = _mapper.Map<ProductViewModel>(response.Data);
                    if (brandsResponse.Succeeded)
                    {
                        var brandViewModel = _mapper.Map<List<BrandViewModel>>(brandsResponse.Data);
                        productViewModel.Brands = new SelectList(brandViewModel, nameof(BrandViewModel.Id), nameof(BrandViewModel.Name), null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", productViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createProductCommand = _mapper.Map<CreateProductCommand>(product);
                    var result = await _mediator.Send(createProductCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Product with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateProductCommand = _mapper.Map<UpdateProductCommand>(product);
                    var result = await _mediator.Send(updateProductCommand);
                    if (result.Succeeded) _notify.Information($"Product with ID {result.Data} Updated.");
                }
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    var image = file.OptimizeImageSize(700, 700);
                    await _mediator.Send(new UpdateProductImageCommand() { Id = id, Image = image });
                }
                var response = await _mediator.Send(new GetAllProductsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteProductCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Product with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllProductsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ProductViewModel>>(response.Data);
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