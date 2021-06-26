using PublicWorkflow.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Domain.Entities.Catalog;

namespace PublicWorkflow.Application.Features.Commands.Update
{
    public class UpdateOrganizationCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string ContactEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string Phone { get; set; }
        public bool? IsDeleted { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateOrganizationCommand, Result<long>>
        {
            private readonly IGenericRepository<Organization> _OrganizationRepository;

            public UpdateProductCommandHandler(IGenericRepository<Organization> OrganizationRepository)
            {
                _OrganizationRepository = OrganizationRepository;
            }

            public async Task<Result<long>> Handle(UpdateOrganizationCommand command, CancellationToken cancellationToken)
            {
                var Organization = await _OrganizationRepository.GetByIdAsync(command.Id);

                if (Organization == null)
                {
                    return Result<long>.Fail($"Organization Not Found.");
                }
                else
                {
                    Organization.Name = command.Name ?? Organization.Name;
                    Organization.Description = command.Description ?? Organization.Description;
                    Organization.Motto = command.Motto ?? Organization.Motto;
                    Organization.Logo = command.Logo ?? Organization.Logo;
                    Organization.ContactEmail = command.ContactEmail ?? Organization.ContactEmail;
                    Organization.Address1 = command.Address1 ?? Organization.Address1;
                    Organization.Address2 = command.Address2 ?? Organization.Address2;
                    Organization.Province = command.Province ?? Organization.Province;
                    Organization.City = command.City ?? Organization.City;
                    Organization.LandMark = command.LandMark ?? Organization.LandMark;
                    Organization.Phone = command.Phone ?? Organization.Phone;
                    Organization.IsDeleted = command.IsDeleted ?? Organization.IsDeleted;
                    await _OrganizationRepository.UpdateAsync(Organization);
                    return Result<long>.Success(Organization.Id,"updated succesfully");
                }
            }
        }
    }
}