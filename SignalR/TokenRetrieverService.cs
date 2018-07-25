using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR
{
    public class TokenRetrieverService
    {
        internal const string TokenItemsKey = "idsrv4:tokenvalidation:token";
        internal const string SignalRTokenKey = "token";

        static Func<HttpRequest, string> AuthHeaderTokenRetriever { get; set; }
        static Func<HttpRequest, string> QueryStringTokenRetriever { get; set; }


        static TokenRetrieverService()
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

            if (string.IsNullOrEmpty(token))
            {
                token = request.HttpContext.Items[TokenItemsKey] as string;
            }

            if (string.IsNullOrEmpty(token) && request.Query.TryGetValue(SignalRTokenKey, out StringValues extract))
            {
                token = extract.ToString();
            }

            return token;
        }
    }
}
