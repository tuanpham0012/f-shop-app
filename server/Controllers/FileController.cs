using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Helpers.Interfaces;

namespace ShopAppApi.Controllers
{
    [ApiController]
    [Route("files")]
    public class FileController(IFileHelper fileHelper) : ControllerBase
    {

        [HttpGet("download/{fileName}")]
        public IActionResult Download(String fileName)
        {
            var fileBytes = fileHelper.Download(fileName);
            if (fileBytes == null)
            {
                return Content("File not found.");
            }
            
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}
