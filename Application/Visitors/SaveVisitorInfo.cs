using Application.Interfaces.Contexts;
using Domain.Visitors;
using MongoDB.Driver;

namespace Application.Visitors
{
    public class SaveVisitorInfo : ISaveVisitorInfo
    {
        private readonly IVisitorDbContext<Visitor> _visitorDbContext;
        private readonly IMongoCollection<Visitor> _visitorCollection;
        public SaveVisitorInfo(IVisitorDbContext<Visitor> visitorDbContext)
        {
            _visitorDbContext = visitorDbContext;
            _visitorCollection = _visitorDbContext.GetCollection();
        }
        public void Execute(RequestSaveVisitorInfoDto request)
        {
            _visitorCollection.InsertOne(new Visitor
            {
                Ip = request.Ip,
                CurrentLink = request.CurrentLink,
                ReferrerLink = request.ReferrerLink,
                Method = request.Method,
                Protocol = request.Protocol,
                PhysicalPath = request.PhysicalPath,
                Browser = new VisitorVersion
                {
                    Family = request.Browser.Family,
                    Version = request.Browser.Version,
                },
                OperatingSystem = new VisitorVersion
                {
                    Family = request.OperatingSystem.Family,
                    Version = request.OperatingSystem.Version
                },
                Device = new Device
                {
                    Brand = request.Device.Brand,
                    Family = request.Device.Family,
                    Model = request.Device.Model,
                    IsSpider = request.Device.IsSpider,
                }
            });
        }
    }
}
