using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DocumentService.Data;
using DocumentService.Models;

namespace DocumentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string tenantId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("TenantId is required");

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            var filePath = Path.Combine(folder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var doc = new Document
            {
                TenantId = tenantId,
                FileName = file.FileName,
                FilePath = filePath,
                UploadedOn = DateTime.UtcNow
            };

            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "File uploaded successfully",
                data = doc
            });
        }

        [HttpGet("{tenantId}")]
        public IActionResult Get(string tenantId)
        {
            var docs = _context.Documents
                .Where(x => x.TenantId == tenantId)
                .OrderByDescending(x => x.Id)
                .ToList();

            return Ok(docs);
        }
    }
}
