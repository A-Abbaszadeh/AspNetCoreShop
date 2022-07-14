using MongoDB.Driver;

namespace Application.Interfaces.Contexts
{
    public interface IVisitorDbContext<T>
    {
        public IMongoCollection<T> GetCollection();
    }
}
