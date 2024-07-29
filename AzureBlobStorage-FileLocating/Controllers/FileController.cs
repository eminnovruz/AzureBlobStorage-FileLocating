using AzureBlobStorage_FileLocating.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureBlobStorage_FileLocating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _storageManager;

        public FileController(IFileService storageManager)
        {
            _storageManager = storageManager;
        }


        [HttpGet("GetUrl")]
        public IActionResult GetUrlAsync(string fileName)
        {
            try
            {
                var result = _storageManager.GetSignedUrl(fileName);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteFileAsync(string fileName)
        {
            try
            {
                var result = await _storageManager.DeleteFileAsync(fileName);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
        }


        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var fileName = Guid.NewGuid().ToString();
                    var contentType = file.ContentType;
                    var result = await _storageManager.UploadFileAsync(stream, fileName, contentType);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
