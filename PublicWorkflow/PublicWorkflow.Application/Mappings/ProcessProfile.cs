using PublicWorkflow.Application.Features.Brands.Commands.Create;
using PublicWorkflow.Application.Features.Brands.Queries.GetAllCached;
using PublicWorkflow.Application.Features.Brands.Queries.GetById;
using PublicWorkflow.Domain.Entities.Catalog;
using AutoMapper;

namespace PublicWorkflow.Application.Mappings
{
    internal class ProcessProfile : Profile
    {
        public ProcessProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}