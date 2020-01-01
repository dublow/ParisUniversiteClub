using System.Linq;
using infrastructure.tables;
using Nancy;
using Nancy.Security;

namespace web.extensions
{
    public static class AuthExtensions
    {
        public static void RequireAdmin(this INancyModule nancyModule)
            => nancyModule.RequiresClaims(x =>
            {
                return x.Subject.Claims
                    .Any(y => 
                        y.Subject.HasClaim(
                            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", 
                            Role.Admin.ToString()));
            });
    }
}
