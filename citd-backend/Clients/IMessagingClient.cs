namespace Citd 
{
    public interface  IMessagingClient {
        void Message<T>(T message);
    }
}