using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace StudentDashboard.Security
{
    public class JwtAuthenticationPass : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;
        public long UserId;
        StudentSessionService _session;
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var request = context.Request;
                var authorization = request.Headers.Authorization;

                if (authorization == null || authorization.Scheme != "Bearer")
                    return;

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                    return;
                }

                var token = authorization.Parameter;
                _session = new StudentSessionService();
                if (token==null ||!await  _session.ValidateSessionAsync(token)){
                    context.ErrorResult = new AuthenticationFailureResult("Token expired", request);
                }
                var principal = await AuthenticateJwtToken(token);

                if (principal == null)
                    context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

                else
                    context.Principal = principal;
            }
            catch (Exception Ex)
            {

            }
        }

        private bool ValidateToken(string token, out string username)
        {
            bool result = false;
            username = null;
            try
            {
                var userId = JwtStudentSessionToken.ValidateToken(token);
                if (userId != -1)
                {
                    result = true;
                    username = userId.ToString();
                }
            }
            catch (Exception Ex)
            {


            }
            return result;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            String username;
            if (ValidateToken(token, out username))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.PrimarySid,username)
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }
        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }

       
    }
}