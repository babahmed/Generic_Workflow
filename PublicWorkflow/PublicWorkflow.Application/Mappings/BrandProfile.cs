using PublicWorkflow.Application.Features.Brands.Commands.Create;
using PublicWorkflow.Application.Features.Brands.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Brands.Queries.GetById;
using PublicWorkflow.Domain.Entities.Catalog;
using AutoMapper;

namespace PublicWorkflow.Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}