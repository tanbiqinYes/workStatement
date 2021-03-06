﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CCWOnlineAPIServer.MessageHandlers
{
    public abstract class BasicAuthenticationHandler : DelegatingHandler
    {
        protected abstract bool Authorize(string username, string password);

        protected abstract string Realm { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization != null && request.Headers.Authorization.Scheme == "Basic")
            {
                var credentials = ParseCredentials(request.Headers.Authorization);

                if (Authorize(credentials.Username, credentials.Password))
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }

            return Task<HttpResponseMessage>.Factory.StartNew(
                () =>
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                    response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));

                    return response;
                });
        }

        private static BasicCredentials ParseCredentials(AuthenticationHeaderValue authHeader)
        {
            try
            {
                var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.ToString().Substring(6)));
                int splitOn = credentials.IndexOf(':');

                return new BasicCredentials
                {
                    Username = credentials.Substring(0, splitOn),
                    Password = credentials.Substring(splitOn + 1)
                };
            }
            catch { }

            return new BasicCredentials();
        }

        internal struct BasicCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}