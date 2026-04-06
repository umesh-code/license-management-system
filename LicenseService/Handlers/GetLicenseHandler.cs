using LicenseService.Data;
using LicenseService.Models;
using LicenseService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LicenseService.Handlers
{
    public class GetLicensesHandler : IRequestHandler<GetLicensesQuery, List<License>>
    {
        private readonly AppDbContext _context;

        public GetLicensesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<License>> Handle(GetLicensesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Licenses
                .Where(x => x.TenantId == request.TenantId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }
    }
}