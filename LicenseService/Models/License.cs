namespace LicenseService.Models
{
    public class License
    {
        public int Id { get; set; }
        public string TenantId { get; set; }   // Multi-tenancy
        public string UserId { get; set; }
        public string LicenseType { get; set; }
        public string Status { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
