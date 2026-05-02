using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpGet("{filename}")]
        public IActionResult GetImage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return BadRequest("Filename is required.");

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images", filename);

            if (!System.IO.File.Exists(imagePath))
                return NotFound("Image not found.");

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            var contentType = GetContentType(imagePath);

            return File(imageBytes, contentType);
        }

        private string GetContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();

            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream",
            };
        }

    }

}

