﻿using AspNetCoreHero.Results;
using MediatR;
using PublicWorkflow.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.ActivityLog.Commands.AddLog
{
    public partial class AddActivityLogCommand : IRequest<Result<int>>
    {
        public string Action { get; set; }
        public string userId { get; set; }
    }

    public class AddActivityLogCommandHandler : IRequestHandler<AddActivityLogCommand, Result<int>>
    {
        private readonly ILogRepository _repo;

        private IUnitOfWork _unitOfWork { get; set; }

        public AddActivityLogCommandHandler(ILogRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AddActivityLogCommand request, CancellationToken cancellationToken)
        {
            await _repo.AddLogAsync(request.Action, request.userId);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(1);
        }
    }
}