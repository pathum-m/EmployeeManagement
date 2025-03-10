using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Application.Abstractions;
public interface IImageService
{
    Task<Result<string>> SaveBase64ImageAsync(string base64String, string containerName, CancellationToken cancellationToken);
}
