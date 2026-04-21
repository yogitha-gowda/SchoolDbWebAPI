using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SchoolDBCoreWebAPI.Models
{
    public class BasicAutheticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        //Database context to query user information during auth
        private readonly SchoolDBContext _context;

        public BasicAutheticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            SchoolDBContext context) : base(options, logger, encoder)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return AuthenticateResult.Fail("Missing Auth Header");
                }
                var authorizationHeader = Request.Headers["Authorization"].ToString();

                if (!AuthenticationHeaderValue.TryParse(authorizationHeader,out var headerValue)){
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }

                if (!"Basic".Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }
                var credentialBytes = Convert.FromBase64String(headerValue.Parameter);
                var credential = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

                if (credential.Length != 2) {
                    return AuthenticateResult.Fail("Invalid Authentication Header");
                }
                var email = credential[0];
                var password = credential[1];

                var user=await _context.Users.SingleOrDefaultAsync(u=>u.Email == email);

                if (user == null || !PasswordHasher.VerifyPasword(user.PassWordHash, password)){
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UsertId.ToString()),
                    new Claim(ClaimTypes.Name,user.Email)
                };

                var roles=user.Role.Split(',',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsIdentity =new ClaimsIdentity(claims,Scheme.Name);
                var claimsPrincipal =new ClaimsPrincipal(claimsIdentity);

                var autheticationTicket =new AuthenticationTicket(claimsPrincipal,Scheme.Name);

                return AuthenticateResult.Success(autheticationTicket);
            }
            catch 
            {
                return AuthenticateResult.Fail("Error Occurred during Authentication");
            }
        }
    }
}
