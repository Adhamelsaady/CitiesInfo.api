using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CitiesApi.Controllers
{
    [Route("api/files")]
    [ApiController]
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

            if (!_fileExtensionContentTypeProvider.TryGetContentType(path, out var contentType)) { contentType = "application/octet-stream"; }

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes , contentType , Path.GetFileName(path));
        }  
    }
}
