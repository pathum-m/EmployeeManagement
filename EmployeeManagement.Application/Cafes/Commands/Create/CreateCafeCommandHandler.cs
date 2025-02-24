using EmployeeManagement.Domain.Abstractions.Repositories;
using MediatR;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Cafes.Commands.Create;
public sealed class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Result<Guid>>
{
    //private readonly ICafeRepository _cafeRepository;

    //public CreateCafeCommandHandler(ICafeRepository cafeRepository) => _cafeRepository = cafeRepository;

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
                return Result.Failure<Guid>(Error.EntityCreationFailure);
            }

            //await _cafeRepository.Add(cafeResult.Value, cancellationToken);
            return cafeResult.Value.Id.Value;
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            return Result.Failure<Guid>(new Error("500", ex.Message));
        }        
    }
}
