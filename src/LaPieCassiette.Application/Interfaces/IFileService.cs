using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface IFileService
{
    Task<string> SaveImageAsync(Stream stream, string fileName);
    void DeleteImage(string path);
}