using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

public class CreateAirplaneCommandValidator : AbstractValidator<CreateAirplaneCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAirplaneCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.CreateAirplaneDto.Model).NotEmpty().WithMessage("Model is required.");
        RuleFor(x => x.CreateAirplaneDto.Manufacturer).NotEmpty().WithMessage("Manufacturer is required.");
        RuleFor(x => x.CreateAirplaneDto.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        RuleFor(x => x.CreateAirplaneDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Matches("^[A-Z]{3}\\d{2}$").WithMessage("Code must be 3 capital letters followed by 2 digits (e.g., ABC12).")
            .MustAsync(async (code, cancellation) =>
            {
                return await _unitOfWork.Airplanes.GetByCodeAsync(code) == null;
            }).WithMessage("An airplane with this code already exists.").WithErrorCode("Conflict");
    }
}