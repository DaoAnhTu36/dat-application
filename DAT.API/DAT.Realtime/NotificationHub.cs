using Microsoft.AspNetCore.SignalR;

namespace DAT.Realtime
{
    public class NotificationHub : Hub
    {
        public async Task SendMessageToClient(string message)
        {
            if (Clients != null)
            {
                await Clients.All.SendAsync("SendMessageFromServerToClient", message);
            }
            Task.CompletedTask.Wait();
        }
    }
}
