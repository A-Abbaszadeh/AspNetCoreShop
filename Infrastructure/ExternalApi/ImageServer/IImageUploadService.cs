using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalApi.ImageServer
{
    public interface IImageUploadService
    {
        List<string> Upload(List<IFormFile> files);
    }

    public class ImageUploadService : IImageUploadService
    {
        public List<string> Upload(List<IFormFile> files)
        {
            var options = new RestClientOptions("https://localhost:44310/api/images?apikey=someSecretKey!!!")
            {
                ThrowOnAnyError = true,
                Timeout = -1
            };
            var client = new RestClient(options);

            var request = new RestRequest();
            foreach (var file in files)
            {
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                request.AddFile(file.FileName, bytes, file.FileName, file.ContentType);
            }

            var response = client.ExecuteAsync(request);
            UploadDto upload = JsonConvert.DeserializeObject<UploadDto>(response.Result.Content);

            return upload.FileNameAddress;
        }
    }

    public class UploadDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }
}
