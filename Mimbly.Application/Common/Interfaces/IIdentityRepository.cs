namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Mimbly.Domain.Enitites;

public interface IIdentityRepository
{
    Task<IEnumerable<Claim>> GetUserClaims(Guid userId);
    Task RegisterUser(string email, string password, string firstName, string lastName);
    Task StoreRefreshToken(string token, Guid userId);
    Task<IEnumerable<string>> GetRefreshTokensByUserId(Guid userId);
    Task<User?> GetUserByEmail(string email);
    Task<User> GetUserByUserId(Guid userId);
    Task UpdateStoredRefreshToken(string oldToken, string newToken, Guid userId);
    Task ChangeUserPassword(Guid userId, string hashedPassword);
    Task LogoutUserCurrentDevice(Guid userId, string refreshToken);
    Task LogoutUserAllDevices(Guid userId);
}