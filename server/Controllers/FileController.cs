using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Helpers.Interfaces;

namespace ShopAppApi.Controllers
{
    [ApiController]
    [Route("files")]
    public class FileController(IFileHelper fileHelper) : ControllerBase
    {

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(String fileName)
        {
            var fileBytes = await fileHelper.Download(fileName);
            if (fileBytes == null)
            {
                return Content("File not found.");
            }

            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet("posts/{slug}")]
        public async Task<IActionResult> Post(string slug)
        {
            var htmlContent = await fileHelper.GetPostContentAsync(slug);
            if (htmlContent == null)
            {
                return NotFound(); // Hoặc trang lỗi 404
            }

            // Truyền nội dung HTML thô vào View
            // Model của View này chỉ cần là một string hoặc một ViewModel chứa string này
            return Ok(new {data = htmlContent});
        }
    }
}
