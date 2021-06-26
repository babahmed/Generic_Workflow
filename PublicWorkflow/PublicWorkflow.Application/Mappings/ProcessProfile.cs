
using PublicWorkflow.Domain.Entities.Catalog;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PublicWorkflow.Application.Features.Commands.Create;
using System.Linq;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetById;

namespace PublicWorkflow.Application.Mappings
{
    internal class ProcessProfile : Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProcessProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var OrgId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c=>c.Type.Contains("OrganizationId"));

            #region ProcessConfig

            CreateMap<CreateProcessConfigCommand, ProcessConfig>()
            .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => long.Parse(OrgId.Value)))
            .ReverseMap();

            CreateMap<GetAllProcessConfigResponse, ProcessConfig>().ReverseMap();
            CreateMap<GetProcessConfigByIdResponse, ProcessConfig>().ReverseMap();

            #endregion


            #region ApprovalConfig

            CreateMap<CreateApprovalConfigCommand, ApprovalConfig>().ReverseMap();
            CreateMap<GetAllApprovalConfigResponse, ApprovalConfig>().ReverseMap();
            CreateMap<GetApprovalConfigByIdResponse, ApprovalConfig>().ReverseMap();

            #endregion


        }
    }
}