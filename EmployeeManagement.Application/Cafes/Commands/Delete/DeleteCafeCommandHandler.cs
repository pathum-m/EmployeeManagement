using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Commands.Delete;
public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, Result<bool>>
{
    private readonly ICafeRepository _cafeRepository;
    private readonly ILogger<DeleteCafeCommandHandler> _logger;

    public DeleteCafeCommandHandler(ICafeRepository cafeRepository,
            ILogger<DeleteCafeCommandHandler> logger)
    {
        _cafeRepository = cafeRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteCafeCommand command, CancellationToken cancellationToken)
    {
        Result<Cafe> cafeResult = await _cafeRepository.GetAsync(command.Id, cancellationToken);

        if (cafeResult.IsFailure)
        {
            _logger.LogWarning("Cafe could not find ID: {CafeId}", command.Id.Value);
            return false;
        }

        _cafeRepository.DeleteAsync(cafeResult.Value);

        _logger.LogInformation("Cafe deleted successfully with ID: {CafeId}", command.Id);

        return true;
    }
}
