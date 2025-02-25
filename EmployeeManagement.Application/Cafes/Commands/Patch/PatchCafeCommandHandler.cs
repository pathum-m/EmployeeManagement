using EmployeeManagement.Domain.Abstractions.Repositories;
using MediatR;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Commands.Patch;
public sealed class PatchCafeCommandHandler : IRequestHandler<PatchCafeCommand, Result<bool>>
{
    private readonly ICafeRepository _cafeRepository;
    private readonly ILogger<PatchCafeCommandHandler> _logger;

    public PatchCafeCommandHandler(ICafeRepository cafeRepository, ILogger<PatchCafeCommandHandler> logger)
    {
        _cafeRepository = cafeRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(PatchCafeCommand command, CancellationToken cancellationToken) 
    {
        try
        {
            Result<Cafe> cafeResult = await _cafeRepository.GetAsync(command.Id, cancellationToken);

            if (cafeResult.IsFailure)
            {
                _logger.LogWarning("Could not find the Cafe ID: {CafeId}", command.Id.Value);
                return false;
            }

            Cafe cafe = cafeResult.Value;
            cafe.Name = command.Name;
            cafe.Description = command.Description;
            cafe.Location = command.Location;

            _cafeRepository.UpdateAsync(cafe, cancellationToken);

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
