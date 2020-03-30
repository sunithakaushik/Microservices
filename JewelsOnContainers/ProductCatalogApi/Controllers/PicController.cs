using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicController : ControllerBase
    {
        // diff b/w readonly, const, static - moudule 12 - time 56 min
        private readonly IWebHostEnvironment _env;
        public PicController(IWebHostEnvironment env)
        {
            _env = env;
        }
        // methods are the api - Module 12 - HTTP get request
        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            var webRoot = _env.WebRootPath;
            var path = Path.Combine($"{webRoot}/pics/", $"Ring{id}.jpg");
            var buffer = System.IO.File.ReadAllBytes(path);
            return File(buffer, "image/jpeg");
        }
    }
}