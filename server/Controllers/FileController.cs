using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppApi.Helpers;

namespace ShopAppApi.Controllers
{
    [ApiController]
    [Route("files")]
    public class FileController(ILogger<FileController> logger) : ControllerBase
    {

        private readonly ILogger<FileController> _logger = logger;

        [HttpGet("download/{fileName}")]
        public IActionResult Download(String fileName)
        {
            var fileBytes = FileHelper.Download(fileName);
            if (fileBytes == null)
            {
                return Content("File not found.");
            }
            
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}
