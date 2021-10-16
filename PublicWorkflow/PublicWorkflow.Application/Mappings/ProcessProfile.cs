
using PublicWorkflow.Domain.Entities.Catalog;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PublicWorkflow.Application.Features.Commands.Create;
using System.Linq;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.DTOs.ViewModel;
using System.Collections.Generic;

namespace PublicWorkflow.Application.Mappings
{
    internal class ProcessProfile : Profile
    {

        public ProcessProfile()
        {

            #region ProcessConfig

            CreateMap<CreateProcessConfigCommand, ProcessConfig>()
            .ReverseMap();
            CreateMap<CreateQuickProcessConfigCommand, ProcessConfig>()
.ReverseMap();

            CreateMap<GetAllProcessConfigResponse, ProcessConfig>().ReverseMap();
            CreateMap<List<GetAllProcessConfigResponse>, List<ProcessConfig>>().ReverseMap();
            CreateMap<GetProcessConfigByIdResponse, ProcessConfig>().ReverseMap();

            #endregion


            #region ApprovalConfig

            CreateMap<CreateApprovalConfigCommand, ApprovalConfig>().ReverseMap();
            CreateMap<GetAllApprovalConfigResponse, ApprovalConfig>().ReverseMap();
            CreateMap<GetApprovalConfigByIdResponse, ApprovalConfig>().ReverseMap();
            CreateMap<ApprovalConfigBase, CreateApprovalConfigCommand>().ReverseMap();
            CreateMap<List<ApprovalConfigBase>, List<CreateApprovalConfigCommand>>().ReverseMap();

            #endregion


        }
    }
}