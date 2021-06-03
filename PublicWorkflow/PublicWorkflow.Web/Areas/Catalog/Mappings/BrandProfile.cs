using PublicWorkflow.Application.Features.Brands.Commands.Create;
using PublicWorkflow.Application.Features.Brands.Commands.Update;
using PublicWorkflow.Application.Features.Brands.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Brands.Queries.GetById;
using PublicWorkflow.Web.Areas.Catalog.Models;
using AutoMapper;

namespace PublicWorkflow.Web.Areas.Catalog.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<GetAllBrandsCachedResponse, BrandViewModel>().ReverseMap();
            CreateMap<GetBrandByIdResponse, BrandViewModel>().ReverseMap();
            CreateMap<CreateBrandCommand, BrandViewModel>().ReverseMap();
            CreateMap<UpdateBrandCommand, BrandViewModel>().ReverseMap();
        }
    }
}