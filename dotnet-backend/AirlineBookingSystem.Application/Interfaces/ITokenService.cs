using System.Threading.Tasks;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Interfaces
{
    /// <summary>
    /// Defines the interface for a service that creates access and refresh tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates an access token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to create the token.</param>
        /// <returns>A JWT access token.</returns>
        string CreateAccessToken(User user);

        /// <summary>
        /// Creates a refresh token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to create the token.</param>
        /// <returns>A refresh token.</returns>
        RefreshToken CreateRefreshToken(User user);
    }
}
