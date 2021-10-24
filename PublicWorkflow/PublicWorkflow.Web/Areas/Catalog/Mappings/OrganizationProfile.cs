
using PublicWorkflow.Web.Areas.Catalog.Models;
using AutoMapper;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Commands.Update;

namespace PublicWorkflow.Web.Areas.Catalog.Mappings
{
    internal class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<GetAllOrganizationResponse, OrganizationViewModel>().ReverseMap();
            CreateMap<GetOrganizationByIdResponse, OrganizationViewModel>().ReverseMap();
            CreateMap<CreateOrganizationCommand, OrganizationViewModel>().ReverseMap();
            CreateMap<UpdateOrganizationCommand, OrganizationViewModel>().ReverseMap();
        }
    }
}