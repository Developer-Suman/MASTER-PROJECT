using Domain.Abstractions;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if (user is null)
            {
                return Result<GetUserByIdResponse>.Failure("Not Found.");
            }

            GetUserByIdResponse getUserByIdResponse = new(
                user.Id,
                user.UserName!,
                user.Email!,
                user.PhoneNumber!
                );

            return Result<GetUserByIdResponse>.Success(getUserByIdResponse);
        }
    }
}
