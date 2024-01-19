using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUserById
{
    public record GetUserByIdResponse(string Id, string username, string email, string phone);
}
