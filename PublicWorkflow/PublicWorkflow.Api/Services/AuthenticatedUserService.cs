using PublicWorkflow.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace PublicWorkflow.Api.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UId = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("uid")) != null ?
            Guid.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("uid")).Value) : null;
            OId = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("oid")) != null ?
                long.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("oid")).Value) : null;
            UserName = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains(JwtRegisteredClaimNames.Email))?.Value;
            FirstName = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("first_name"))?.Value;
            LastName = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("last_name"))?.Value;
        }
        public Guid? UId { get; }
        public string UserName { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public long? OId { get; }
    }
}