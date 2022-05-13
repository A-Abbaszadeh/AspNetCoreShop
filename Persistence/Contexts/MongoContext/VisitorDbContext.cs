using Application.Interfaces.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts.MongoContext
{
    public class VisitorDbContext<T> : IVisitorDbContext<T>
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T> visitorCollection;
        public VisitorDbContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("VisitorDb");
            visitorCollection = db.GetCollection<T>(typeof(T).Name);
        }
        public IMongoCollection<T> GetCollection()
        {
            return visitorCollection;
        }
    }
}
