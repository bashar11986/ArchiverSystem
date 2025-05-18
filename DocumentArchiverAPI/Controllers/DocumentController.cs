using DocumentArchiverAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentArchiverAPI.Controllers
{
    [Route("archiver/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly AppDbContext _db;
        public DocumentController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> allDocuments()
        {
            var docs = await _db.Documents.ToListAsync();
            return Ok(docs);
        }
    }
}
