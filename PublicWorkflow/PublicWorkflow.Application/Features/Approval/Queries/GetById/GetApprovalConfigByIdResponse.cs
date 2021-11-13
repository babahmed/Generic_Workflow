using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using System.Collections;
using System.Collections.Generic;

namespace PublicWorkflow.Application.Features.Queries.GetById
{
    public class GetApprovalConfigByIdResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProcessConfigId { get; set; }
        public int Level { get; set; }
        public int RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }
        public List<GetAllApprovalRuleResponse> Rules { get; set; }
    }
}