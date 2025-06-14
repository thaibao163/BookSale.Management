﻿using Microsoft.AspNetCore.Http;

namespace BookSale.Management.Application
{
    public interface IImageService
    {
        Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName);
    }
}