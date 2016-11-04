using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using Rx;

[HubName("messaging")]
public class MessagingHub : Hub
{
    private Rx.IMessagePublisher _messages;

    public MessagingHub(Rx.IMessagePublisher messages) 
    {
        _messages = messages;
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