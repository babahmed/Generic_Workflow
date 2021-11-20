﻿using PublicWorkflow.Application.Constants;
using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Application.Interfaces.Service;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Infrastructure.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IGenericRepository<ApprovalConfig> _ApprovalConfigRepository;
        private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;
        private readonly IGenericRepository<Process> _ProcessRepository;
        private readonly IGenericRepository<Approval> _ApprovalRepository;
        private readonly IGenericRepository<ProcessRule> _ProcessRuleRepository;
        private readonly IGenericRepository<ApprovalRule> _ApprovalRuleRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IDateTimeService _date;
        private readonly IPublishService _publisher;

            public ProcessService(
                IGenericRepository<ApprovalConfig> _ApprovalConfigRepository,
                IGenericRepository<ProcessConfig> _ProcessConfigRepository,
            IGenericRepository<Process> _ProcessRepository,
            IGenericRepository<Approval> _ApprovalRepository,
            IGenericRepository<ProcessRule> _ProcessRuleRepository,
            IGenericRepository<ApprovalRule> _ApprovalRuleRepository,
            IGenericRepository<History> _historyRepository,
            IDateTimeService _date,
            IPublishService _publisher
                )
            {
                this._ApprovalConfigRepository = _ApprovalConfigRepository;
                this._ProcessConfigRepository = _ProcessConfigRepository;
                this._ProcessRepository = _ProcessRepository;
                this._ApprovalRepository = _ApprovalRepository;
                this._ProcessRuleRepository = _ProcessRuleRepository;
                this._ApprovalRuleRepository = _ApprovalRuleRepository;
                this._date = _date;
                this._historyRepository = _historyRepository;
                this._publisher = _publisher;
            }


            public async Task PostApproval(UpdateLevelCommand request, IAuthenticatedUserService user)
        {
            bool Processcomplete=false;
            var logs = new List<History>();

            var Approval = await _ApprovalRepository.GetByIdAsync(request.ApprovalId);
            var ApprovalConfig = await _ApprovalConfigRepository.GetByIdAsync(Approval.ApprovalconfigId);
            var process = await _ProcessRepository.GetAsync(c => c.Id == Approval.ProcessId);
            var processConfig = await _ProcessConfigRepository.GetAsync(c => c.Id == process.ProcessConfigId);
            //Load the level rules
            var approvalRules = await _ApprovalRuleRepository.GetAllAsync(c => c.ApprovalConfigId == ApprovalConfig.Id && !c.IsDeleted);
            var processRules = await _ProcessRuleRepository.GetAllAsync(c => c.ProcessConfigId == process.Id && !c.IsDeleted);

            //Apply Apporoval level rules
            //if no rule is set....Apply default
            if (!approvalRules.Any()
                //or request is rejection and no rejecction rule....Apply default
                || (request.Status == Status.Rejected && !approvalRules.Where(c=>c.Action==RuleAction.Reject).Any())
                //or request is approval and no approval rule....Apply default
                || (request.Status == Status.Approved && !approvalRules.Where(c => c.Action == RuleAction.Approve).Any()))
            {
                //if request is to reject
                if (request.Status == Status.Rejected)
                {
                    //Update Approvals
                    Approval.Status = Status.Rejected;
                    Approval.Treated = true;
                    Approval.Actioned = _date.NowUtc;

                }

                //Has everyone approved?
                if (ApprovalConfig.Approvers.Length == Approval.AlreadyApproved.Length && request.Status == Status.Approved)
                {
                    Approval.Actioned = _date.NowUtc;
                    Approval.Treated = true;
                }
            }
            else
            {
                //1 must count means minimum
                //2 can count means least
                //3 must user means required
                //4 can user means suffice

                if (request.Status == Status.Rejected)
                {

                    if (
                        //check if we have any approval rule
                        approvalRules.Where(c=>c.Action==RuleAction.Reject).Any()
                        &&
                        //check if we have any count condtion and its satisfied ie minimum count of rejection user requirement
                        (approvalRules.Where(c => c.Action == RuleAction.Reject && c.Type==RuleType.Count).Any() &&
                            //Derive the number of people who alreayd rejected
                            approvalRules.All(c => c.Action == RuleAction.Reject && c.Type == RuleType.Count && int.Parse(c.Values[0]) >= (Approval.AlreadyActioned.Length - Approval.AlreadyApproved.Length)))
                        &&
                        //check if we have a users condtion and its satisfied ie least count of rejection user requirement
                        (approvalRules.Where(c => c.Action == RuleAction.Reject && c.Type == RuleType.User).Any() &&
                            //Derive the number of people who alreayd rejected
                            approvalRules.All(c => c.Action == RuleAction.Reject && c.Type == RuleType.User && c.Values.All(c=> Approval.AlreadyApproved.Contains(c))))

                        )
                    {
                        Approval.Status = Status.Rejected;
                        Approval.Treated = true;
                        Approval.Actioned = _date.NowUtc;
                    }
                }

                if (request.Status == Status.Approved)
                {

                    if (
                        //check if we have any approval rule
                        approvalRules.Where(c => c.Action == RuleAction.Approve).Any()
                        &&
                        //check if we have any count condtion and its satisfied ie minimum count of approval user requirement
                        (approvalRules.Where(c => c.Action == RuleAction.Approve && c.Type == RuleType.Count).Any() &&
                            //Derive the number of people who alreayd rejected
                            approvalRules.All(c => c.Action == RuleAction.Approve && c.Type == RuleType.Count && int.Parse(c.Values[0]) >= Approval.AlreadyApproved.Length))
                        &&
                        //check if we have a users condtion and its satisfied ie least count of approval user requirement
                        (approvalRules.Where(c => c.Action == RuleAction.Approve && c.Type == RuleType.User).Any() &&
                            //Derive the number of people who alreayd rejected
                            approvalRules.All(c => c.Action == RuleAction.Approve && c.Type == RuleType.User && c.Values.All(c => Approval.AlreadyApproved.Contains(c))))

                        )
                    {
                        Approval.Actioned = _date.NowUtc;
                        Approval.Treated = true;

                        //TODO: Implement notification for non concurrent level process
                    }
                }

            }


            //Apply Process level rule
            if (!processRules.Any())
            {
                //If no rule set on process, apply default
                if (Approval.Status==Status.Rejected)
                {
                    //Update process
                    process.Status = Status.Rejected;
                    process.Completed = _date.NowUtc;
                    Processcomplete = true;

                    logs.Add(new History()
                    {
                        Action = Messages.ProcessJob.CompletedRejected,
                        ProcessId = process.Id,
                        Username = user.UserName,
                        ApprovalId = Approval.Id
                    });
                }

                var totalApproved = await _ApprovalRepository.CountAsync(c => c.Status == Status.Approved && c.ProcessId==Approval.ProcessId);
                if (Approval.Status == Status.Approved && totalApproved+1 == processConfig.RequiredApprovalLevels)
                {
                    //Update process
                    process.Status = Status.Approved;
                    process.Completed = _date.NowUtc;
                    Processcomplete = true;

                    logs.Add(new History()
                    {
                        Action = Messages.ProcessJob.CompletedRejected,
                        ProcessId = process.Id,
                        Username = user.UserName,
                        ApprovalId = Approval.Id
                    });
                }
            }
            else
            {
                //TODO Implement rule on Process config

            }

            //Check if Job Level is complete

            await _ApprovalRepository.UpdateAsync(Approval);
            if (Processcomplete) { await _ProcessRepository.UpdateAsync(process); }
        }
    }
}