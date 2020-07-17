using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace edwreportsmvc
{
    public class MyHub :Hub
    {
        public void Announce(string message)
        {
            message = "This message was sent from the server: " + message;
            Clients.Client(Context.ConnectionId).Announce(message);
            //Clients.All.Announce(message);
        }
    }
}