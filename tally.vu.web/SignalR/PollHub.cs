using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using System.Threading.Tasks;

namespace tallyvu
{
    public class PollHub : Hub, IDisconnect
    {
        public void Join(string key)
        {
            Groups.Add(Context.ConnectionId, key);
        }

        public Task Disconnect()
        {
            return Groups.Remove(Context.ConnectionId, "");
        }
    }
}