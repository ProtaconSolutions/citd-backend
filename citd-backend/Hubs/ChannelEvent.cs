using System;
using Newtonsoft.Json;

public class ChannelEvent
{
    public string Name { get; set; }
    public string ChannelName { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public object Data
    {
        get { return _data; }
        set
        {
            _data = value;
            this.Json = JsonConvert.SerializeObject(_data);
        }
    }
    private object _data;

    public string Json { get; private set; }

    public ChannelEvent()
    {
        Timestamp = DateTimeOffset.Now;
    }
}