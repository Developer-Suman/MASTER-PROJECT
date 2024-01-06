using Domain.Abstractions;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IJwtProvider _jwtProvider;

        public RefreshTokenCommandHandler(IAuthenticationRepository authenticationRepository, IJwtProvider jwtProvider)
        {
            _authenticationRepository = authenticationRepository;
            _jwtProvider = jwtProvider;
            
        }
        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(request.token);

            if(principal is null)
            {
                return Result<RefreshTokenResponse>.Failure("Invalid Token");
            }

            string username = principal.Identity!.Name!;

            if(username is null)
            {
                return Result<RefreshTokenResponse>.Failure("Invalid Token");
            }

            var user = await _authenticationRepository.FindByNameAsync(username);

            if(user is null || user.RefreshToken != request.refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Result<RefreshTokenResponse>.Failure("Invalid access token or refresh Token");
            }

            var roles = await _authenticationRepository.GetRolesAsync(user);
            var newToken =  _jwtProvider.Genetrate(user, roles);

            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _authenticationRepository.UpdateUserAsync(user);

            RefreshTokenResponse refreshTokenResponse = new(
                newToken,
                newRefreshToken
                );
            return Result<RefreshTokenResponse>.Success(refreshTokenResponse);
        }
    }
}
