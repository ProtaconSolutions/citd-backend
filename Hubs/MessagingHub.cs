using System;
using System.Threading.Tasks;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using Rx;

[HubName("messaging")]
public class MessagingHub : Hub
{
    private IMessagePublisher _messages;
    private static MessagingOutAdapter _adapter; 

    public MessagingHub(IMessagePublisher messages) 
    {
        _messages = messages;
        
        // This is ugly workaround for injecting hub context to another class.
        if(_adapter == null)
            _adapter = new MessagingOutAdapter(_messages, this);
    }

    public async Task Subscribe(string channel)
    {
        await Groups.Add(Context.ConnectionId, channel);

        var ev = new ChannelEvent
        {
            ChannelName = Constants.AdminChannel,
            Name = "user.subscribed",
            Data = new
            {
                Context.ConnectionId,
                ChannelName = channel
            }
        };

        Publish(ev);
    }

    public async Task Unsubscribe(string channel)
    {
        await Groups.Remove(Context.ConnectionId, channel);

        var ev = new ChannelEvent
        {
            ChannelName = Constants.AdminChannel,
            Name = "user.unsubscribed",
            Data = new
            {
                Context.ConnectionId,
                ChannelName = channel
            }
        };

        Publish(ev);
    }


    public void Publish(ChannelEvent channelEvent)
    {
        _messages.Publish<RequestMessage>(
            new RequestMessage(channelEvent.ChannelName, Context.ConnectionId, channelEvent.Data));
    }

    public override Task OnConnected()
    {
        var ev = new ChannelEvent
        {
            ChannelName = Constants.AdminChannel,
            Name = "user.connected",
            Data = new
            {
                Context.ConnectionId,
            }
        };

        Publish(ev);

        return base.OnConnected();
    }

    public override Task OnDisconnected(bool stopCalled)
    {
        var ev = new ChannelEvent
        {
            ChannelName = Constants.AdminChannel,
            Name = "user.disconnected",
            Data = new
            {
                Context.ConnectionId,
            }
        };

        Publish(ev);

        return base.OnDisconnected(stopCalled);
    }
}