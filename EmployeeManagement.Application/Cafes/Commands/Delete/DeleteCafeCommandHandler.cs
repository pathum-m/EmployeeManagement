using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Commands.Delete;
public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCafeCommandHandler> _logger;

    public DeleteCafeCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteCafeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteCafeCommand command, CancellationToken cancellationToken)
    {

        try
        {
            Result<Cafe> cafeResult = await _unitOfWork.Cafes.GetAsync(command.Id, cancellationToken);
            if (cafeResult.IsFailure)
            {
                _logger.LogWarning("Cafe could not find ID: {CafeId}", command.Id.Value);
                return Result.Failure<bool>(Error.NotFound);
            }

            _unitOfWork.Cafes.DeleteAsync(cafeResult.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Cafe deleted successfully with ID: {CafeId}", command.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred when deleting a cafe : {ErrorMessage}", ex.Message);
            return Result.Failure<bool>(new Error("500", ex.Message));
        }
    }
}
