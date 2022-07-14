namespace Application.Visitors.OnlineVisitors
{
    public interface IOnlineVisitorService
    {
        void ConnectUser(string ClientId);
        void DisconnectUser(string ClientId);
        int GetCount();
    }
}
