using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateProfilePicture;

public class UpdateProfilePictureCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment) : IRequestHandler<UpdateProfilePictureCommand, Result>
{
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

        user.Person.ImagePath = Path.Combine("/uploads", "profile_pictures", uniqueFileName).Replace('\\', '/');
        

        unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();

        return Result.NoContent();
    }
}