using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace StaticFile.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public ImagesController(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }

        public IActionResult Post(string apiKey)
        {
            if (apiKey != "someSecretKey!!!")
            {
                return BadRequest();
            }

            try
            {
                var files = Request.Form.Files;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files is not null)
                {
                    // Upload files
                    return Ok(Upload(files));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
                throw new Exception("upload image error", ex);
            }
        }

        private UploadDto Upload(IFormFileCollection files)
        {
            string newName = Guid.NewGuid().ToString();
            var date = DateTime.Now;
            string folder = $@"Resources\Images\{date.Year}\{date.Year} - {date.Month}\";
            var uploadRootFolder = Path.Combine(_environment.WebRootPath, folder);

            if (!Directory.Exists(uploadRootFolder))
            {
                Directory.CreateDirectory(uploadRootFolder);
            }
            List<string> address = new List<string>();
            foreach (var file in files)
            {
                if (file is not null && file.Length > 0)
                {
                    string fileName = $"{newName}_{file.FileName}";
                    var filePath = Path.Combine(uploadRootFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    address.Add(folder + fileName);
                }
            }

            return new UploadDto
            {
                FileNameAddress = address,
                Status = true
            };
        }
    }

    public class UploadDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }
}
