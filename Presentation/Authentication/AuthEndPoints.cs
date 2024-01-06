using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.RefreshToken;
using Application.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Authentication
{
    public static class AuthEndPoints
    {
        public static void MapAuthEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("Auth/login", async ([FromBody] LoginRequest request, IMediator _mediator, CancellationToken cancellationtoken) =>
            {
                try
                {
                    var command = new LoginCommand(request.username, request.password);
                    var tokenResult = await _mediator.Send(command, cancellationtoken);

                    return tokenResult.IsSuccess ? Results.Ok(tokenResult.Data) : Results.BadRequest(tokenResult.Errors);

                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            }).WithTags("Authenticate");

            app.MapPost("Auth/get-refresh-token", async ([FromBody] RefreshTokenRequest request, IMediator _mediator, CancellationToken cancellationtokrn) =>
            {
                try
                {
                    var command = new RefreshTokenCommand(request.token, request.refreshToken);
                    var result = await _mediator.Send(command, cancellationtokrn);

                    return result.IsSuccess? Results.Ok(result.Data) : Results.BadRequest(result.Errors);

                }catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            }).WithTags("Authenticate");

            app.MapPost("Auth/register-user", async ([FromBody] RegisterRequest request, IMediator _mediator, CancellationToken cancellationtoken) =>
            {
                try
                {
                    var command = new RegisterCommand(request.username,request.email, request.password);

                    var result= await _mediator.Send(command,cancellationtoken);
                    return result.IsSuccess ? Results.Ok(result.Data) :Results.BadRequest(result.Errors);

                }catch(Exception ex)
                {
                    return Results.BadRequest(ex.Message) ;
                }

            }).WithTags("Authenticate");
        }
    }
}
