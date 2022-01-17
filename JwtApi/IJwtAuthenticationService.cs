using JwtApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtApi
{
    public interface IJwtAuthenticationService
    {
        User Authenticate(string email, string password);
        string GenerateToken(string secret, List<Claim> claims);
    }
}
