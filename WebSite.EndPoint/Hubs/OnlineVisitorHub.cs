using Application.Visitors.OnlineVisitors;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebSite.EndPoint.Hubs
{
    public class OnlineVisitorHub:Hub
    {
        private readonly IOnlineVisitorService _onlineVisitorService;
        public OnlineVisitorHub(IOnlineVisitorService onlineVisitorService)
        {
            _onlineVisitorService = onlineVisitorService;
        }

        public override Task OnConnectedAsync()
        {
            var visitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _onlineVisitorService.ConnectUser(visitorId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var visitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _onlineVisitorService.DisconnectUser(visitorId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
