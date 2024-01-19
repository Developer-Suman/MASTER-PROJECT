using Domain.Abstractions;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetAllUsers;
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public async Task<Result<List<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUser();

            List<GetAllUsersResponse> getAllUserResponse = new List<GetAllUsersResponse>();
            if (users is not null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    getAllUserResponse.Add(new GetAllUsersResponse(
                        user.Id,
                        user.UserName!,
                        user.Email!,
                        user.PhoneNumber!));
                }
            }

            return Result<List<GetAllUsersResponse>>.Success(getAllUserResponse);
        }
    }
