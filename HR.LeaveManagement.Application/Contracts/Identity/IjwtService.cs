using HR.LeaveManagement.Application.Models.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IjwtService
    {
        Tokens GenerateToken(IEnumerable<Claim> claims);
        Tokens GenerateRefreshToken(IEnumerable<Claim> claims);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
