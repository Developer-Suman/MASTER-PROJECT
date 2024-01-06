using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Login
{
    public record LoginResponse(string token, string refreshtoken);
    
}
