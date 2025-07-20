using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace AirlineBookingSystem.Application.Features.Users.Commands.DeleteProfilePicture;

public class DeleteProfilePictureCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment) : IRequestHandler<DeleteProfilePictureCommand, Result>
{
    public async Task<Result> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.NotFound("User not found.");
        }

        if (string.IsNullOrEmpty(user.Person.ImagePath))
        {
            return Result.NoContent(); // No profile picture to delete
        }

        var filePath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", user.Person.ImagePath.TrimStart('/'));

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        user.Person.ImagePath = null;
        

        unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();

        return Result.NoContent();
    }
}
