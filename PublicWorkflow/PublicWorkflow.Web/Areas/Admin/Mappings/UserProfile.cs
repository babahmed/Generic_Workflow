using PublicWorkflow.Infrastructure.Identity.Models;
using PublicWorkflow.Web.Areas.Admin.Models;
using AutoMapper;

namespace PublicWorkflow.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}