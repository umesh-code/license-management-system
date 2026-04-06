using LicenseService.Commands;
using LicenseService.Data;
using LicenseService.Models;
using MediatR;

namespace LicenseService.Handlers
{
    public class CreateLicenseHandler : IRequestHandler<CreateLicenseCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateLicenseHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLicenseCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.TenantId) ||
                string.IsNullOrEmpty(request.UserId) ||
                string.IsNullOrEmpty(request.LicenseType))
            {
                throw new Exception("Invalid input data");
            }

            var license = new License
            {
                TenantId = request.TenantId,
                UserId = request.UserId,
                LicenseType = request.LicenseType,
                Status = "Pending",
                ExpiryDate = DateTime.UtcNow.AddYears(1)
            };

            _context.Licenses.Add(license);
            await _context.SaveChangesAsync();

            return license.Id;
        }
    }
}