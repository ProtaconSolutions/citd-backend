using System;
using Microsoft.AspNetCore.SignalR;

namespace Hubs
{
    public class MessagingOutAdapter
    {
        public MessagingOutAdapter(Rx.IMessagePublisher messages, IHubContext<MessagingHub> hub)
        {
            messages.OfType<ResponseMessage>()
                .Subscribe(x =>
                {
                    hub.Clients.Client(x.ConnectionId).OnEvent(x.Type, new ChannelEvent
                    {
                        Name = x.Type,
                        ChannelName = x.Type,
                        Data = x.Data
                    });
                });

            messages.OfType<BroadcastMessage>()
                .Subscribe(x =>
                {
                    hub.Clients.Group(x.Type).OnEvent(x.Type, new ChannelEvent
                    {
                        Name = x.Type,
                        ChannelName = x.Type,
                        Data = x.Data
                    });
                });
        }
    }
}