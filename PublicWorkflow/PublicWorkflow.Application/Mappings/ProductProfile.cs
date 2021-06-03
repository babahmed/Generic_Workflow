using PublicWorkflow.Application.Features.Products.Commands.Create;
using PublicWorkflow.Application.Features.Products.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Products.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Products.Queries.GetById;
using PublicWorkflow.Domain.Entities.Catalog;
using AutoMapper;

namespace PublicWorkflow.Application.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsCachedResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        }
    }
}