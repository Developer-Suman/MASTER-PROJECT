using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(string token, string refreshToken) : IRequest<Result<RefreshTokenResponse>>;
}
