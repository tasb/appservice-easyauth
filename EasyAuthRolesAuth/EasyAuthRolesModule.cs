using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

namespace EasyAuthRolesAuth
{
    public class EasyAuthRolesModule : IHttpModule
    {
        public void Dispose()
        {
            //nothing to dispose.
            return;
        }

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += Context_PostAuthenticateRequest;
        }

        private void Context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if ((app != null) && (app.User != null))
            {
                if (app.User.Identity.IsAuthenticated && app.User is ClaimsPrincipal)
                {
                    var rolesClaims = new List<Claim>();
                    foreach (var c in ((ClaimsIdentity)app.User.Identity).Claims)
                    {
                        if (c.Type.Equals("roles"))
                        {
                            rolesClaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, c.Value));
                        }
                    }
                    ((ClaimsPrincipal)app.User).AddIdentity(new ClaimsIdentity(rolesClaims));
                }
            }
            System.Diagnostics.Trace.TraceInformation("Bu!");
        }
    }
}

