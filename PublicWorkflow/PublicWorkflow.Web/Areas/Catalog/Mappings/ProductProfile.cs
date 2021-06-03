using PublicWorkflow.Application.Features.Products.Commands.Create;
using PublicWorkflow.Application.Features.Products.Commands.Update;
using PublicWorkflow.Application.Features.Products.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Products.Queries.GetById;
using PublicWorkflow.Web.Areas.Catalog.Models;
using AutoMapper;

namespace PublicWorkflow.Web.Areas.Catalog.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsCachedResponse, ProductViewModel>().ReverseMap();
            CreateMap<GetProductByIdResponse, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductViewModel>().ReverseMap();
        }
    }
}