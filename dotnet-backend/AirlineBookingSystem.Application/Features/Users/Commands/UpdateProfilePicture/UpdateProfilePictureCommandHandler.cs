using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateProfilePicture;

/// <summary>
/// Handles the update of a user's profile picture.
/// </summary>
public class UpdateProfilePictureCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment) : IRequestHandler<UpdateProfilePictureCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="UpdateProfilePictureCommand"/> to update a user's profile picture.
    /// This involves deleting any existing profile picture, saving the new picture to the file system,
    /// and updating the user's image path in the database.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.NotFound("User not found.");
        }

        var uploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "uploads", "profile_pictures");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Delete old profile picture if it exists
        if (!string.IsNullOrEmpty(user.Person.ImagePath))
        {
            var oldFilePath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", user.Person.ImagePath.TrimStart('/'));
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
        }

        // Generate a unique filename using GUID
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await File.WriteAllBytesAsync(filePath, request.FileContent, cancellationToken);

        user.Person.ImagePath = "/uploads/profile_pictures/" + uniqueFileName; // Corrected line
        
        unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();

        return Result.NoContent();
    }
}