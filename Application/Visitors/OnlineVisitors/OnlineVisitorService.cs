using Application.Interfaces.Contexts;
using Domain.Visitors;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Application.Visitors.OnlineVisitors
{
    public class OnlineVisitorService : IOnlineVisitorService
    {
        private readonly IVisitorDbContext<OnlineVisitor> _visitorDbContext;
        private readonly IMongoCollection<OnlineVisitor> _onlineVisitorCollection;

        public OnlineVisitorService(IVisitorDbContext<OnlineVisitor> visitorDbContext)
        {
            _visitorDbContext = visitorDbContext;
            _onlineVisitorCollection = _visitorDbContext.GetCollection();
        }

        public void ConnectUser(string ClientId)
        {
            var exist = _onlineVisitorCollection.AsQueryable().FirstOrDefault(p => p.ClientId == ClientId);
            if (exist is null)
            {
                _onlineVisitorCollection.InsertOne(new OnlineVisitor
                {
                    ClientId = ClientId,
                    Time = DateTime.Now
                });
            }
        }

        public void DisconnectUser(string ClientId)
        {
            _onlineVisitorCollection.FindOneAndDelete(p => p.ClientId == ClientId);
        }

        public int GetCount()
        {
            return _onlineVisitorCollection.AsQueryable().Count();
        }
    }
}
