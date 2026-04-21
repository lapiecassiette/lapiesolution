using System;
using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;

using Microsoft.AspNetCore.Http;
public class FileService : IFileService
{
    private readonly string _imageFolder;

    public FileService()
    {
        _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
    }

    public async Task<string> SaveImageAsync(Stream stream, string fileName)
    {
        if (stream == null || stream.Length == 0)
            throw new ArgumentException("File is empty");

        if (!Directory.Exists(_imageFolder))
            Directory.CreateDirectory(_imageFolder);

        var extension = Path.GetExtension(fileName).ToLower();

        // 🔥 sécurité minimale
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        if (!allowedExtensions.Contains(extension))
            throw new Exception("Invalid file type");

        var newFileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(_imageFolder, newFileName);

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await stream.CopyToAsync(fileStream);
        }

        return $"/images/{newFileName}";
    }

    public async Task DeleteImageAsync(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return;

        var fullPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            path.TrimStart('/')
        );

        if (File.Exists(fullPath))
        {
            // 🔥 petit async safe
            await Task.Run(() => File.Delete(fullPath));
        }
    }

    public void DeleteImage(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return;

        var fullPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            path.TrimStart('/')
        );

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}