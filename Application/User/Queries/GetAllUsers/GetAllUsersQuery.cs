using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<Result<List<GetAllUsersResponse>>>;
}
    
