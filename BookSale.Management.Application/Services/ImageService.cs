using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class ImageService : IImageService
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;

        //public ImageService(IWebHostEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}

        private readonly string _webRootPath;

        public ImageService(string webRootPath)
        {
            _webRootPath = webRootPath;
        }


        public async Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName)
        {
            try
            {
                //check image
                if (images?.Count == 0 || string.IsNullOrEmpty(path))
                {
                    return default;
                }

                //string pathImage = Path.Combine(_webHostEnvironment.WebRootPath, path); //images/user

                string pathImage = Path.Combine(_webRootPath, path);

                //check pathImage
                if (!Directory.Exists(pathImage))
                {
                    Directory.CreateDirectory(pathImage);
                }

                foreach (var image in images)
                {
                    string originalPath = Path.Combine(pathImage, !string.IsNullOrEmpty(defaultName) ? defaultName : image.Name);

                    //save image
                    using (var fileStream = new FileStream(originalPath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception)
            {
                return default;
            }

            return true;
        }
    }
}
