using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Commands.Patch;
public sealed class PatchCafeCommandHandler : IRequestHandler<PatchCafeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PatchCafeCommandHandler> _logger;

    public PatchCafeCommandHandler(IUnitOfWork unitOfWork, ILogger<PatchCafeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(PatchCafeCommand command, CancellationToken cancellationToken) 
    {
        try
        {
            Result<Cafe> cafeResult = await _unitOfWork.Cafes.GetAsync(command.Id, cancellationToken);
            if (cafeResult.IsFailure)
            {
                _logger.LogWarning("Could not find the Cafe ID: {CafeId}", command.Id.Value);
                return false;
            }

            Cafe cafe = cafeResult.Value;
            Result<bool> updateResult = cafe.UpdateDetails(command.Name, command.Description, command.Location, command.Logo);
            if (updateResult.IsFailure)
            {
                _logger.LogError("Updating cafe {CafeName} has failed: ", command.Name);
                return false;
            }

            _unitOfWork.Cafes.UpdateAsync(cafe, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Cafe updated successfully with ID: {CafeId}", cafe.Id.Value);
            return true;
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            _logger.LogCritical(ex, "Exception updating cafe with ID: {CafeId}", command.Id.Value);
            return Result.Failure<bool>(new Error("500", ex.Message));
        }        
    }
}
