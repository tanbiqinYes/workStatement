using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Security.Principal;
using System.DirectoryServices;
using CCWOnlineAPI.Management.Entities;
using CCWOnlineAPI.Management.Repository;
using CCWOnlineAPI.Management.Models;

namespace CCWOnlineAPIServer.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        public PER_AccountRepository accountRepository = new PER_AccountRepository();
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //验证域账户
            //ApplicationUser user = null;
            //if (CheckDomianAccount("RootDSE", "wu_zheng", "WZQwe1234567"))
            //{
            //    user = await userManager.FindAsync(context.UserName, context.Password);
            //}
            Account user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "用户名或密码不正确。");
                return;
            }

            //获取当前用户信息
            //Account account = accountRepository.FindOne(c=>c.DomainName == context.UserName);

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // 资源所有者密码凭据未提供客户端 ID。
            if (context.ClientId == null)
            {
                context.Validated("self");
                //context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        public static bool CheckDomianAccount(string DominName, string Useranem, string Password)
        {
            using (DirectoryEntry deUser = new DirectoryEntry(@"LDAP://" + DominName, Useranem.Trim(), Password.Trim()))
            {
                DirectorySearcher src = new DirectorySearcher(deUser);
                src.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + Useranem.Trim() + "))";
                src.PropertiesToLoad.Add("cn");
                src.SearchRoot = deUser;
                src.SearchScope = SearchScope.Subtree;

                SearchResult result = src.FindOne();
                if (result != null)//登录成功
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
    }
}