using Domain.Abstractions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IAuthenticationRepository authenticationRepository, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationRepository.FindByNameAsync(request.username);
            if(user is null)
            {
                return Result<LoginResponse>.Failure("Unauthorized: Invalid Credentials");
            }

            if(!await _authenticationRepository.CheckPasswordAsync(user, request.password))
            {
                return Result<LoginResponse>.Failure("Unauthorized : Invalid Credentials");
            }

            var roles = await _authenticationRepository.GetRolesAsync(user);

            string token = _jwtProvider.Genetrate(user, roles);

            string refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);


            await _authenticationRepository.UpdateUserAsync(user);

            var loginResponse = new LoginResponse(
                token, 
                refreshToken
                );

            return Result<LoginResponse>.Success(loginResponse);



        }
    }
}
