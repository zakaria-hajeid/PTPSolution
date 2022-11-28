using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PTP.Hubs.UserHub
{
    public class UserHub : Hub
    {
        public async Task RegisterUser( string message)
        {
            await Clients.All.SendAsync("RegisterUserevent", message);
            
        }
    }
}
