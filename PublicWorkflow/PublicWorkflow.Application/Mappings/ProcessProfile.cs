
using AspNetCoreHero.Results;
using AutoMapper;
using PublicWorkflow.Application.DTOs.ViewModel;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Application.Features.Commands.Create;
using PublicWorkflow.Application.Features.Queries.GetAll;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
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
            CreateMap<PaginatedResult<GetAllApprovalConfigResponse>, PaginatedResult<ApprovalConfig>>().ReverseMap();

            //Response
            CreateMap<GetApprovalConfigByIdResponse, ApprovalConfig>().ReverseMap();

            //Response List
            CreateMap<GetAllApprovalConfigResponse, ApprovalConfig>().ReverseMap();
            CreateMap<List<GetAllApprovalConfigResponse>, List<ApprovalConfig>>().ReverseMap();

            CreateMap<ApprovalConfigBase, CreateApprovalConfigCommand>().ReverseMap();
            CreateMap<List<ApprovalConfigBase>, List<CreateApprovalConfigCommand>>().ReverseMap();

            #endregion

            #region Rule

            CreateMap<CreateApprovalRuleCommand, ApprovalRule>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToEnum<RuleType>()))
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToEnum<Rulecondition>()))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action.ToEnum<RuleAction>()))
                .ReverseMap();

            CreateMap<CreateProcessRuleCommand, ProcessRule>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToEnum<RuleType>()))
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToEnum<Rulecondition>()))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action.ToEnum<RuleAction>()))
                .ReverseMap();
            //CreateMap<GetAllApprovalConfigResponse, ApprovalConfig>().ReverseMap();
            CreateMap<List<CreateProcessRuleCommand>, List<ProcessRule>>().ReverseMap();
            CreateMap<List<CreateApprovalRuleCommand>, List<ApprovalRule>>().ReverseMap();
            //CreateMap<GetApprovalConfigByIdResponse, ApprovalConfig>().ReverseMap();
            //CreateMap<ApprovalConfigBase, CreateApprovalConfigCommand>().ReverseMap();
            //CreateMap<List<ApprovalConfigBase>, List<CreateApprovalConfigCommand>>().ReverseMap();

            #endregion


        }
    }
}