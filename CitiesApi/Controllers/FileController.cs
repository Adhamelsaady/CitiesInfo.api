using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading.Tasks;

namespace CitiesApi.Controllers
{
    [Route("api/v{version:ApiVersion}/files")]
    [Authorize]
    [ApiController]
    [ApiVersion(0.1 , Deprecated = true)]
    public class FileController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new System.ArgumentNullException(nameof (fileExtensionContentTypeProvider));
        }

        [HttpGet("{FileId}")]
        public ActionResult GetFile (int FileId)
        {
            var path = "getting-acquainted-with-aspnet-core-slides.pdf";
            if(!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(path, out var contentType)) 
            { 
                contentType = "application/octet-stream"; 
            }

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes , contentType , Path.GetFileName(path));
        }

        [HttpPost]
        public async Task<ActionResult> CreateFile(IFormFile file)
        {
            if(file.Length == 0 || file.Length > 100000000 || file.ContentType != "application/pdf")
            {
                return BadRequest("No file or Invalid one has been inputted");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory() , $"uploaded_file_{Guid.NewGuid()}.pdf");
            using (var stream = new FileStream(path , FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("File has been created");
        }
    }
}
