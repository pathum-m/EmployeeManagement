using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Commands.Create;
public sealed class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCafeCommandHandler> _logger;
    private readonly IImageService _imageService;

    public CreateCafeCommandHandler(IUnitOfWork unitOfWork, IImageService imageService, ILogger<CreateCafeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateCafeCommand command, CancellationToken cancellationToken) 
    {
        try
        {
            string? logoPath = null;

            if (!string.IsNullOrEmpty(command.Logo))
            {
                Result<string> logoResult = await _imageService.SaveBase64ImageAsync(command.Logo, "cafe-logos", cancellationToken);
                if (logoResult.IsFailure)
                {
                    _logger.LogError("Could not save cafe logo. Cafe name: {CafeName}", command.Name);
                    return Result.Failure<Guid>(Error.ImageSavingFailed);
                }
                logoPath = logoResult.Value;
            }

            Result<Cafe> cafeResult =Cafe.Create(
                command.Name,
                command.Description,
                command.Location,
                logoPath);

            if (cafeResult.IsFailure)
            {
                _logger.LogError("Creating new cafe has failed: {CafeName}", command.Name);
                return Result.Failure<Guid>(Error.EntityCreationFailure);
            }

            await _unitOfWork.Cafes.AddAsync(cafeResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Cafe created successfully with ID: {CafeId}", cafeResult.Value.Id);
            return cafeResult.Value.Id.Value;
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            _logger.LogError(ex, "Exception occurred when saving new cafe : {ErrorMessage}", ex.Message);
            return Result.Failure<Guid>(new Error("500", ex.Message));
        }        
    }
}
