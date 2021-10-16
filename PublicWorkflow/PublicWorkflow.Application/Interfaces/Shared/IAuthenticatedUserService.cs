using System;

namespace PublicWorkflow.Application.Interfaces.Shared
{
    public interface IAuthenticatedUserService
    {
        public Guid? UId { get; }
        public string UserName { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public long? OId { get; }
    }
}