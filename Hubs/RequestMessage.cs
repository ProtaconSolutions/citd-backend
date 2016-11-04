namespace Hubs 
{
    public class RequestMessage 
    {
        public RequestMessage(string type, string sender, object data) 
        {
            Type = type;
            ConnectionId = sender;
            Data = data;
        }

        public string Type;
        public string ConnectionId;
        public dynamic Data;
    }
}