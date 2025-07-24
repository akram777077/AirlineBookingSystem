using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

public class UpdateAirplaneCommandValidator : AbstractValidator<UpdateAirplaneCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAirplaneCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(x => x.UpdateAirplaneDto.Model).NotEmpty().WithMessage("Model is required.");
        RuleFor(x => x.UpdateAirplaneDto.Manufacturer).NotEmpty().WithMessage("Manufacturer is required.");
        RuleFor(x => x.UpdateAirplaneDto.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        RuleFor(x => x.UpdateAirplaneDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Matches("^[A-Z]{3}\\d{2}$").WithMessage("Code must be 3 capital letters followed by 2 digits (e.g., ABC12).")
            .MustAsync(async (command, code, cancellation) =>
            {
                var existingAirplane = await _unitOfWork.Airplanes.GetByCodeAsync(code);
                return existingAirplane == null || existingAirplane.Id == command.Id;
            }).WithMessage("An airplane with this code already exists.").WithErrorCode("Conflict");
    }
}