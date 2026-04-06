using LicenseService.Models;
using MediatR;

namespace LicenseService.Queries
{
    public record GetLicensesQuery(string TenantId) : IRequest<List<License>>;
}
