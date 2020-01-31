using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebServerStudy.Core.Hub
{
    public class PeerToServerHub : Microsoft.AspNetCore.SignalR.Hub
    {
        [Authorize]
        public void SendToAll(string handler, Guid id, string jsonData)
        {
            Clients.All.SendAsync(handler, id, jsonData);
        }

        [Authorize]
        public void SendToRoom()
        {
            
        }
        
        [Authorize]
        public void SendToPlayer()
        {
            
        }
    }
}