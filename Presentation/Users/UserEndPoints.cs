using Application.User.Queries.GetAllUsers;
using Application.User.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Users
{
    public static class UserEndPoints
    {
        public static void MapUserEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("User/get-all-users", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (IMediator _mediator, CancellationToken cancellationToken) =>
            {
                try
                {
                    var query = new GetAllUsersQuery();

                    var response = await _mediator.Send(query, cancellationToken);

                    return response.IsSuccess ? Results.Ok(response.Data) : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            }).WithTags("User");

            app.MapGet("User/get-by-id", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (string Id, IMediator _mediator, CancellationToken cancellationToken) =>
            {
                try
                {
                    var query = new GetUserByIdQuery(Id);

                    var response = await _mediator.Send(query, cancellationToken);

                    return response.IsSuccess ? Results.Ok(response.Data) : Results.NotFound(response.Errors);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithTags("User");
        }
    }
}
