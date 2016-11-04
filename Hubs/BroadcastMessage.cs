namespace Hubs 
{
    public class BroadcastMessage 
    {
        public BroadcastMessage(string type, object data) 
        {
            Type = type;
            Data = data;
        }

        public string Type;
        public dynamic Data;
    }
}