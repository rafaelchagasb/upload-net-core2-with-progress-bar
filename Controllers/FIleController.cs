using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace teste.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1209715200)]
        [RequestSizeLimit(1209715200)]
        public async Task<ActionResult> Post(IFormFile file)
        {
            FileStream filestream = new FileStream("/tmp/" + file.FileName, FileMode.Create, FileAccess.Write);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
             
                memoryStream.WriteTo(filestream);
            }

            return Ok(new { count = 1, file.FileName, file.Length});
        }

    }

}