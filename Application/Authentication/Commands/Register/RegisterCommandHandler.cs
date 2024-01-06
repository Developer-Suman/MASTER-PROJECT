using Domain.Abstractions;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public RegisterCommandHandler(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
            
        }

        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _authenticationRepository.FindByNameAsync(request.username);
            if( userExists is not null)
            {
                return Result<string>.Failure("User already Exists");
            }

            var userEmailExists = await _authenticationRepository.FindByEmailAsync(request.email);
            if(userEmailExists is not null)
            {
                return Result<string>.Failure("User email Already Exists");
            }

            ApplicationUser user = new()
            {
                UserName = request.username,
                Email = request.email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _authenticationRepository.CreateAsync(user, request.password);
            if(!result.Succeeded)
            {
                return Result<string>.Failure("User Creation Failed.");
            }

            return Result<string>.Success();
        }
    }
}
