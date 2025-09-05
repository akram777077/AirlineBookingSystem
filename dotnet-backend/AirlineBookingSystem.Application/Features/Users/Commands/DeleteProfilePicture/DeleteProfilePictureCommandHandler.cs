using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace AirlineBookingSystem.Application.Features.Users.Commands.DeleteProfilePicture;

/// <summary>
/// Handles the deletion of a user's profile picture.
/// </summary>
public class DeleteProfilePictureCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment) : IRequestHandler<DeleteProfilePictureCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="DeleteProfilePictureCommand"/> to delete a user's profile picture.
    /// This involves removing the file from the file system and updating the user's image path in the database.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
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
