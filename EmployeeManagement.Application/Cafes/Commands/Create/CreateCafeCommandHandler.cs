using EmployeeManagement.Domain.Abstractions.Repositories;
using MediatR;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Commands.Create;
public sealed class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Result<Guid>>
{
    private readonly ICafeRepository _cafeRepository;
    private readonly ILogger<CreateCafeCommandHandler> _logger;

    public CreateCafeCommandHandler(ICafeRepository cafeRepository, ILogger<CreateCafeCommandHandler> logger)
    {
        _cafeRepository = cafeRepository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateCafeCommand command, CancellationToken cancellationToken) 
    {
        try
        {
            Result<Cafe> cafeResult =Cafe.Create(
                command.Name,
                command.Description,
                command.Location,
                command.Logo);

            if (cafeResult.IsFailure)
            {
                _logger.LogError("Creating new cafe has failed: {CafeName}", command.Name);
                return Result.Failure<Guid>(Error.EntityCreationFailure);
            }

            await _cafeRepository.AddAsync(cafeResult.Value, cancellationToken);
            _logger.LogInformation("Cafe created successfully with ID: {CafeId}", cafeResult.Value.Id);
            return cafeResult.Value.Id.Value;
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            _logger.LogCritical(ex, "Exception saving new cafe");
            return Result.Failure<Guid>(new Error("500", ex.Message));
        }        
    }
}
