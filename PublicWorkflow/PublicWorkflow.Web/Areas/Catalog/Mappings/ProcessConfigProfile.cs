using PublicWorkflow.Application.Features.ProcessConfigs.Commands.Update;
using PublicWorkflow.Web.Areas.Catalog.Models;
using AutoMapper;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.Features.Queries.GetAll;

namespace PublicWorkflow.Web.Areas.Catalog.Mappings
{
    internal class ProcessConfigProfile : Profile
    {
        public ProcessConfigProfile()
        {
            CreateMap<GetAllProcessConfigResponse, ProcessConfigViewModel>().ReverseMap();
            CreateMap<GetProcessConfigByIdResponse, ProcessConfigViewModel>().ReverseMap();
            CreateMap<CreateProcessConfigCommand, ProcessConfigViewModel>().ReverseMap();
            CreateMap<UpdateProcessConfigCommand, ProcessConfigViewModel>().ReverseMap();
        }
    }
}