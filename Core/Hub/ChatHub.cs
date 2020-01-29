using Microsoft.AspNetCore.SignalR;

namespace WebServerStudy.Core.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}