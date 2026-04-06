using LicenseService.Data;

namespace LicenseService.Jobs
{
    public class RenewalJob
    {
        private readonly AppDbContext _context;

        public RenewalJob(AppDbContext context)
        {
            _context = context;
        }

        public void CheckExpiry()
        {
            var expiringLicenses = _context.Licenses
                .Where(x => x.ExpiryDate <= DateTime.Now.AddDays(30))
                .ToList();

            foreach (var license in expiringLicenses)
            {
                Console.WriteLine($"License {license.Id} expiring soon!");
            }
        }
    }
}
