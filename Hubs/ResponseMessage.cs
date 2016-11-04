namespace Hubs 
{
    public class ResponseMessage 
    {
        public ResponseMessage(string type, string participant, object data) 
        {
            Type = type;
            Data = data;
            ConnectionId = participant;
        }

        public string Type;
        public string ConnectionId;
        public dynamic Data;
    }
}