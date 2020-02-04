using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Graphene.SharedModels.Network;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace WebServerStudy.Core.Hub
{
    public class PeerToServerHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly NetworkClients _connections;

        public PeerToServerHub()
        {
            _connections = new NetworkClients("");
        }
        
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            string id = Context.ConnectionId;

            _connections.Add(name, id);

            Clients.All.SendAsync("OnConnected", name, id);
            
            foreach (var client in _connections.Clients)
            {
                Clients.Client(id).SendAsync("OnClientUpdate", client);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;
            string id = Context.ConnectionId;

            _connections.Remove(name, id);

            try
            {
                Clients.All.SendAsync("OnDisconnected", name, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return base.OnDisconnectedAsync(exception);
        }


        [Authorize]
        public void SendToAll(string handler, string userId, string jsonData)
        {
            Clients.All.SendAsync(handler, userId, jsonData);
        }

        [Authorize]
        public void SendToAllFromId(string handler, Guid id, string jsonData)
        {
            Clients.All.SendAsync(handler, id, jsonData);
        }

        [Authorize]
        public void UpdatePlayer(NetworkClient client)
        {
            _connections.Update(client);

            Clients.All.SendAsync("OnClientUpdate", client);
        }


        [Authorize]
        public void SendToRoom()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public void SendToPlayer()
        {
            throw new NotImplementedException();
        }
    }
}