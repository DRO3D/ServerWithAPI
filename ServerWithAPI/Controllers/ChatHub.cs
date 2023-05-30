using Microsoft.AspNetCore.SignalR;

namespace ServerWithAPI.Controllers
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendUserMessage(string user, string message)
        {
            await Clients.User(user).SendAsync(message);
        }

        public async Task SendGroupMessage(string user, string message)
        {

        }
    }

}
