using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Helpers;

public static class ClaimsExtensions
{
    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
}
