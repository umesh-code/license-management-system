namespace DocumentService.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}
