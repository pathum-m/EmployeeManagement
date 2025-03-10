using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Shared;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeManagement.Infrastructure.Services;
public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;

    public ImageService(IWebHostEnvironment environment) => _environment = environment;

    public async Task<Result<string>> SaveBase64ImageAsync(string base64String, string containerName, CancellationToken cancellationToken) 
    {
        if (string.IsNullOrEmpty(base64String))
        {
            Result.Failure<string>(Error.Null);
        }

        string fileName = $"{Guid.NewGuid()}.jpg";

        string directoryPath = Path.Combine(_environment.WebRootPath, containerName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        byte[] bytes = Convert.FromBase64String(base64String);
        string filePath = Path.Combine(directoryPath, fileName);

        await File.WriteAllBytesAsync(filePath, bytes);

        return $"/{containerName}/{fileName}";
    }
}
