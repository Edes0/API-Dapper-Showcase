namespace Boilerplate.Api.Controllers.Identity;

using System.Security.Claims;
using Application.Commands.Identity.ChangePassword;
using Application.Commands.Identity.LoginUser;
using Application.Commands.Identity.LogoutUserAll;
using Application.Commands.Identity.LogoutUserCurrent;
using Application.Commands.Identity.RefreshAccessToken;
using Application.Commands.Identity.RegisterUser;
using Application.Contracts.Requests.Identity;
using CoreServices.Exceptions;
using FollowUp.Api.Controllers;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class IdentityController : BaseController
{
    private readonly ITokenHandler _tokenHandler;

    public IdentityController(ITokenHandler tokenHandler, IMediator mediator) : base(mediator) => _tokenHandler = tokenHandler;

    [HttpPost] //api/v1/identity/register
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequestDto registerUserRequestDto)
    {
        await _mediator.Send(
            new RegisterUserCommand
            {
                RegisterUserRequestDto = registerUserRequestDto
            });

        return Ok("Successfully registered");
    }

    [HttpDelete] //api/v1/identity/deleteAccount
    [Authorize]
    [Route("deleteAccount")]
    public async Task<ActionResult> DeleteAccount()
    {
        await Task.Run(() => Console.WriteLine("Create functionality to delete user account and related data (preferably through cascade configuration"));
        throw new NotImplementedException();
    }

    [HttpPost] //api/v1/identity/login
    [Route("login")]
    public async Task<ActionResult<LoginUserVm>> Login(LoginUserRequestDto loginUserRequestDto)
    {
        return Ok(await _mediator.Send(
            new LoginUserCommand
            {
                LoginUserRequestDto = loginUserRequestDto
            }));
    }

    [HttpPost] //api/v1/identity/refresh
    [Route("refresh")]
    public async Task<ActionResult> RefreshAccessToken(RefreshAccessTokenDto refreshAccessTokenDto)
    {
        var userId = _tokenHandler.GetUserIdFromAccessToken(refreshAccessTokenDto.AccessToken);
        _ = userId == null ? throw new BadRequestException("No valid user id could be extracted from token. Therefore refresh failed.") : true;

        return Ok(await _mediator.Send(
            new RefreshAccessTokenCommand
            {
                UserId = userId,
                RefreshToken = refreshAccessTokenDto.RefreshToken
            }));
    }

    // Logs out and clears refresh token from current device.
    [HttpPost] //api/v1/identity/logoutCurrent
    [Authorize]
    [Route("logoutCurrent")]
    public async Task<ActionResult> LogoutCurrent(LogoutSingleDeviceRequestDto logoutSingleDeviceDto)
    {
        // DELETE ROW THAT MATCHES REFRESH TOKEN AND USERID.
        var currentUser = HttpContext.User;
        var userId = currentUser.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier)?.Value;
        _ = userId == null ? throw new BadRequestException("No valid user id could be extracted from token when attempting to logout.") : true;

        await _mediator.Send(
            new LogoutUserCurrentCommand
            {
                UserId = Guid.Parse(userId),
                RefreshToken = logoutSingleDeviceDto.RefreshToken
            });

        return Ok("Logged out successfully");
    }

    // Logs out and clears refresh token from all devices.
    [HttpPost] //api/v1/identity/logoutAll
    [Authorize]
    [Route("logoutAll")]
    public async Task<ActionResult> LogoutAll()
    {
        // DELETE ALL ROWS WITH USERID.
        var currentUser = HttpContext.User;
        var userId = currentUser.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier)?.Value;
        _ = userId == null ? throw new BadRequestException("No valid user id could be extracted from token when attempting to logout.") : true;

        await _mediator.Send(
            new LogoutUserAllCommand
            {
                UserId = Guid.Parse(userId)
            });

        return Ok("Logged out from all devices successfully");
    }

    [HttpPost] //api/v1/identity/changePassword
    [Authorize]
    [Route("changePassword")]
    public async Task<ActionResult> ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
    {
        var currentUser = HttpContext.User;
        var userId = currentUser.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier)?.Value;
        _ = userId == null ? throw new BadRequestException("No valid user id could be extracted from token when attempting to change password.") : true;

        await _mediator.Send(
            new ChangePasswordCommand
            {
                UserId = Guid.Parse(userId),
                ChangePasswordData = changePasswordRequestDto
            });

        return Ok("Password changed successfully");
    }

    [HttpPost] //api/v1/user/identity/requestPasswordReset
    [Route("requestPasswordReset")]
    public async Task<ActionResult> ResetPasswordByEmail()
    {
        await Task.Run(() => Console.WriteLine("Implement functionality to get a link sent to email with token to reset password."));
        throw new NotImplementedException();
    }

    [HttpPost] //api/v1/identity/resetPasswordWithToken
    [Route("resetPasswordWithToken")]
    public async Task<ActionResult> ResetPasswordWithToken
        ()
    {
        await Task.Run(() => Console.WriteLine("Implement functionality to reset password with token sent in link through email."));
        throw new NotImplementedException();
    }
}