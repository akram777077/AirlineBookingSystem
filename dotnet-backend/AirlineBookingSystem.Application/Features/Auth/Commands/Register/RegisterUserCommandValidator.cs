using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Auth.Commands.Register;

/// <summary>
/// Validator for the <see cref="RegisterUserCommand"/>.
/// </summary>
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserCommandValidator"/> class.
    /// </summary>
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Username).MinimumLength(3).WithMessage("Username must be at least 3 characters long.");
        RuleFor(x => x.Username).MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.Password).MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
        RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.");
        RuleFor(x => x.Password).Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.");
        RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Password must contain at least one digit.");
        RuleFor(x => x.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
        RuleFor(x => x.DateOfBirth).Must(BeAValidAge).WithMessage("User must be at least 18 years old.");

        RuleFor(x => x.GenderId).NotEmpty().WithMessage("Gender is required.");
        RuleFor(x => x.CityId).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code is required.");
    }

    private bool BeAValidAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }
        return age >= 18;
    }
}
