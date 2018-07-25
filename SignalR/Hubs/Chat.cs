using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Hubs
{   
    [Authorize]
    public class Chat : Hub
    {
        public async Task SendPrivateMessage(string sender, string message)
        {
            var xx = Context;            
           
            await Clients.All.SendAsync("RecievedMessage", new { sender = sender, message = message});
        }

        public override async Task OnConnectedAsync()
        {            
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
