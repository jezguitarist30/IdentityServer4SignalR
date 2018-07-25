using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalR
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var p = connection.User?.Identity?.Name;
            return "E77BC7A5-46F7-4648-8FDD-D67D6820A3AE";// connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
