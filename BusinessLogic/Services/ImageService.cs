﻿using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Net;
using System.Net.Http;

namespace BusinessLogic.Services
{
    internal class ImageService: IImageService
    {
        
        private readonly IConfiguration config;
        private readonly string imgPath;

        public ImageService(IWebHostEnvironment env,
            IConfiguration config)
        {
            this.config = config;
            imgPath = Path.Combine(env.WebRootPath, config["ImgDir"] ?? string.Empty);
            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            using MemoryStream ms = new();
            await image.CopyToAsync(ms);
            string fileName = await SaveImageAsync(ms.ToArray());
            return fileName;
        }

        public async Task<List<string>> SaveImagesAsync(IEnumerable<IFormFile> images)
        {
            List<string> result = [];

            try
            {
                foreach (var image in images)
                {
                    result.Add(await SaveImageAsync(image));
                }
            }
            catch (Exception)
            {
                result.ForEach(DeleteImageIfExists);
                throw new HttpException("Error image save", HttpStatusCode.InternalServerError);
            }

            return result;
        }

        public async Task<string> SaveImageAsync(string base64)
        {
            if (base64.Contains(','))
                base64 = base64.Split(',')[1];

            var bytes = Convert.FromBase64String(base64);

            var fileName = await SaveImageAsync(bytes);

            return fileName;
        }

        public async Task<string> SaveImageAsync(byte[] bytes)
        {
            List<int> sizes = config.GetRequiredSection("ImageSizes").Get<List<int>>()
                ?? throw new Exception("ImageSizes reading error");

            if (sizes.Count == 0)
                throw new Exception("ImageSizes not inicialized");

            string imageName = $"{Path.GetRandomFileName()}.webp";

            var tasks = sizes
                .Select(s => SaveImageAsync(bytes, imageName, s))
                .ToArray();

            await Task.WhenAll(tasks);

            return imageName;
        }

        private async Task SaveImageAsync(byte[] bytes, string name, int size)
        {
            string dirSaveImage = Path.Combine(imgPath, $"{size}_{name}");

            using var image = Image.Load(bytes);
            image.Mutate(imageProcessingContext =>
            {
                imageProcessingContext.Resize(new ResizeOptions
                {
                    Size = new Size(Math.Min(image.Width, size), Math.Min(image.Height, size)),
                    Mode = ResizeMode.Max
                });
            });

            using var stream = File.Create(dirSaveImage);
            await image.SaveAsync(stream, new WebpEncoder());
        }

        public async Task<List<string>> SaveImagesAsync(IEnumerable<byte[]> bytesArrays)
        {
            List<string> result = [];

            try
            {
                foreach (var bytes in bytesArrays)
                {
                    result.Add(await SaveImageAsync(bytes));
                }
            }
            catch (Exception)
            {
                result.ForEach(DeleteImageIfExists);
                throw new HttpException("Error image save", HttpStatusCode.InternalServerError);
            }

            return result;
        }

        public async Task<byte[]> LoadBytesAsync(string name)
        {
            return await File.ReadAllBytesAsync(Path.Combine(imgPath, name));
        }


        public void DeleteImage(string nameWithFormat)
        {
            foreach (var size in Sizes)
            {
                File.Delete(Path.Combine(imgPath, $"{size}_{nameWithFormat}"));
            }
        }

        public void DeleteImages(IEnumerable<string> images)
        {
            foreach (var image in images)
                DeleteImage(image);
        }

        public void DeleteImageIfExists(string nameWithFormat)
        {
            foreach (var size in Sizes)
            {
                var path = Path.Combine(imgPath, $"{size}_{nameWithFormat}");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public void DeleteImagesIfExists(IEnumerable<string> images)
        {
            foreach (var image in images)
                DeleteImageIfExists(image);
        }

        public async Task<string> SaveImageByUrlAsync(string imageUrl)
        {
            using HttpClient httpClient = new();
            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
            return await SaveImageAsync(imageBytes);
        }

        private List<int> Sizes
        {
            get
            {
                List<int> sizes = config.GetRequiredSection("ImageSizes").Get<List<int>>()
                ?? throw new HttpException("ImageSizes reading error", HttpStatusCode.InternalServerError);

                if (sizes.Count == 0)
                    throw new HttpException("ImageSizes not inicialized", HttpStatusCode.InternalServerError);
                return sizes;
            }
        }

    }
}
