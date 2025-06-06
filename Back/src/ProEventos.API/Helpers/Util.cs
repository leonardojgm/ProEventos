using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Helpers
{
    public class Util : IUtil
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public Util(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;            
        }

        public async Task<string> SaveImage(IFormFile imageFile, string destino)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                .Take(10)
                .ToArray()
            ).Replace(" ", "-");

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @$"Resources/{destino}", imageName);

            using (var fileStrem = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStrem);
            }

            return imageName;
        }

        public void DeleteImage(string imageName, string destino)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @$"Resources/{destino}", imageName);

                if (File.Exists(imagePath)) 
                    File.Delete(imagePath);
            }
        }
    }
}