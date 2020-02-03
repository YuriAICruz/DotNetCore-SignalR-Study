using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Graphene.SharedModels.Network;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebServerStudy.Core.Hub
{
    public class PeerToServerHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private NetworkClients _connections = new NetworkClients();

        [Authorize]
        public void SendToAll(string handler, Guid id, string jsonData)
        {
            Clients.All.SendAsync(handler, id, jsonData);
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            string id = Context.ConnectionId;

            _connections.Add(name, id);

            Clients.All.SendAsync("OnConnected", name, id);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;
            string id = Context.ConnectionId;

            _connections.Remove(name, id);
            
            Clients.All.SendAsync("OnDisconnected", name, id);
            
            return base.OnDisconnectedAsync(exception);
        }

        [Authorize]
        public void ConnectionInfo(string name)
        {
            Console.WriteLine(
                "----------------------------------------" + name + "  -  " + Clients.User(name));
            //Clients.AllExcept(name).SendAsync("OnConnected", name);
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