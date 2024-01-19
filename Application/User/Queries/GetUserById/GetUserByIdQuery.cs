using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUserById
{
    public record GetUserByIdQuery(string Id) : IRequest<Result<GetUserByIdResponse>>;

}
