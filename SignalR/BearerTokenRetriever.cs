using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Http;
using System;

namespace SignalR
{
    public class BearerTokenRetriever
    {
        static Func<HttpRequest, string> AuthHeaderTokenRetriever { get; set; }
        static Func<HttpRequest, string> QueryStringTokenRetriever { get; set; }

        static BearerTokenRetriever()
        {
            AuthHeaderTokenRetriever = TokenRetrieval.FromAuthorizationHeader();
            QueryStringTokenRetriever = TokenRetrieval.FromQueryString();
        }

        public static string FromHeaderAndQueryString(HttpRequest request)
        {
            var token = AuthHeaderTokenRetriever(request);

            if (string.IsNullOrEmpty(token))
            {
                token = QueryStringTokenRetriever(request);
            }

            return token;
        }
    }
}
