using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.RefreshToken
{
    public record RefreshTokenResponse(string token, string refreshToken);
   
}
