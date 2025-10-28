using DocumentArchiverAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DocumentArchiverAPI.Controllers
{
    [ApiController]
    [Route("archiver/[controller]")]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;
        public DocumentController(IWebHostEnvironment env, AppDbContext db)
        {
            _env = env;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> allDocuments()
        {
            var docs = await _db.Documents.ToListAsync();
            return Ok(docs);
        }

        [HttpPost("upload")]
        //  public async Task<IActionResult> uploadFile([FromForm] IFormFile file, [FromForm] string title, [FromForm] int categoryId)
        public async Task<IActionResult> uploadFile([FromForm] string title, [FromForm] int categoryId)
        {
            var username = User.Identity.Name;
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (file == null || file.Length == 0) return BadRequest("the file not uploaded");
            //var uploadsFolder = Path.Combine(_env.ContentRootPath, "Uploads");
            //if (!Directory.Exists(uploadsFolder))
            //    Directory.CreateDirectory(uploadsFolder);
            //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            //var filePath = Path.Combine(uploadsFolder, fileName);
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}
            var document = new Models.Document
            {
                //  Id = 1,
                Title = title,
                FileName = "file.FileName",
                // FileName = file.FileName,
                FilePath = "fileName",
                // FilePath = fileName,
                dateCreate = DateTime.UtcNow,
                CategoryId = categoryId,
                UserCreate = username

                //  UserCreate = /* استخرج من JWT عبر HttpContext.User.Claims */
                //  UserCreate =  HttpContext.User.Claims,
                // Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)
            };
            _db.Documents.Add(document);
            await _db.SaveChangesAsync();

            return Ok(new { document.Id });
        }
    }// end class    
}
